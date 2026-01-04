using System.Linq;
using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using Poker.Enums;

namespace Poker.Implementation;

public class Deal
{
  private List<Card> _cards { get; }
  public string Cards => ToString();
  public Rank Rank { get; set; }
  public string RankName => Rank.ToString();
  public string RankDescription => Rank.GetDisplayName();
  public string RankCombo { get; private set; }


  public Deal()
  {
    _cards = new List<Card>();
    RankCombo = string.Empty;
    Rank = Rank.Unknown;
  }

  public void AddCard(Card card)
  {
    _cards.Add(card);
  }

  public override string ToString()
  {
    return "│" + string.Join('│', _cards) + "│";
  }

  public void Check()
  {
    if (_cards.Count != 5)
    {
      Rank = Rank.Unknown;
      RankCombo = string.Empty;
      return;
    }

    var result = EvaluateBestHand();
    Rank = result.Rank;
    RankCombo = result.Combo;
  }

  public bool CheckFlush()
  {
    var jokerCount = _cards.Count(card => card.Face == Face.Jocker);
    var suitCounts = _cards
      .Where(card => card.Face != Face.Jocker)
      .GroupBy(card => card.Suit)
      .Select(group => group.Count())
      .ToArray();

    var maxSuit = suitCounts.Any() ? suitCounts.Max() : 0;
    return maxSuit + jokerCount >= _cards.Count && _cards.Count > 0;
  }

  public bool CheckStraight()
  {
    if (_cards.Count == 0)
    {
      return false;
    }

    var jokerCount = _cards.Count(card => card.Face == Face.Jocker);
    var faces = _cards.Where(card => card.Face != Face.Jocker).Select(card => card.Face).ToList();

    if (faces.GroupBy(face => face).Any(group => group.Count() > 1))
    {
      return false;
    }

    var straightSequences = GetStraightSequences();

    return straightSequences.Any(sequence =>
      faces.All(sequence.Contains) && sequence.Length - faces.Count <= jokerCount);
  }

  public Card GetHighCard()
  {
    var evaluation = EvaluateBestHand();

    return evaluation.HighCard;
  }

  private EvaluationResult EvaluateBestHand()
  {
    var jokerCount = _cards.Count(card => card.Face == Face.Jocker);
    var baseCards = _cards.Where(card => card.Face != Face.Jocker).ToList();

    return jokerCount switch
    {
      0 => EvaluateHand(baseCards),
      1 => EvaluateHandWithJokers(baseCards, 1),
      _ => EvaluateHandWithJokers(baseCards, 2)
    };
  }

  private EvaluationResult EvaluateHandWithJokers(List<Card> baseCards, int jokerCount)
  {
    var suits = Enum.GetValues<Suit>().Where(suit => suit != Suit.Jocker).ToArray();
    var faces = Enum.GetValues<Face>().Where(face => face != Face.Jocker).ToArray();

    EvaluationResult? bestResult = null;

    if (jokerCount == 1)
    {
      foreach (var face in faces)
      foreach (var suit in suits)
      {
        var cards = baseCards.Append(new Card(face, suit)).ToList();
        var current = EvaluateHand(cards);
        bestResult = SelectBetterResult(bestResult, current);
      }
    }
    else
    {
      foreach (var firstFace in faces)
      foreach (var firstSuit in suits)
      foreach (var secondFace in faces)
      foreach (var secondSuit in suits)
      {
        var cards = baseCards
          .Concat(new[]
          {
            new Card(firstFace, firstSuit),
            new Card(secondFace, secondSuit)
          })
          .ToList();

        var current = EvaluateHand(cards);
        bestResult = SelectBetterResult(bestResult, current);
      }
    }

    return bestResult ?? EvaluateHand(baseCards);
  }

  private EvaluationResult EvaluateHand(List<Card> cards)
  {
    var orderedFaces = cards.Select(card => card.Face).OrderBy(face => face).ToList();
    var isStraight = IsStraight(orderedFaces, out var straightHighFace, out var straightOrder, out var isWheel);
    var isFlush = IsFlush(cards);

    var grouped = cards
      .GroupBy(card => card.Face)
      .Select(group => (Face: group.Key, Count: group.Count()))
      .OrderByDescending(group => group.Count)
      .ThenByDescending(group => group.Face)
      .ToArray();

    if (grouped[0].Count == 5)
    {
      return CreateResult(Rank.FiveOfKind, grouped[0].Face.GetDisplayName(), cards,
        BuildRankValues(grouped, false), GetCardForFace(cards, grouped[0].Face));
    }

    if (isFlush && isStraight)
    {
      var rank = orderedFaces.First() == Face.Ten && straightHighFace == Face.Ace && !isWheel
        ? Rank.RoyalFlush
        : Rank.StraightFlush;

      return CreateResult(rank, GetCardForFace(cards, straightHighFace).ToString(), cards,
        BuildRankValuesFromFaces(straightOrder, isWheel), GetCardForFace(cards, straightHighFace));
    }

    if (grouped.Length == 2)
    {
      var rank = grouped[0].Count == 4 ? Rank.Quads : Rank.FullHouse;
      var combo = grouped[0].Face.GetDisplayName();

      if (rank == Rank.FullHouse)
      {
        combo += $",{grouped[1].Face.GetDisplayName()}";
      }

      return CreateResult(rank, combo, cards, BuildRankValues(grouped, false),
        GetCardForFace(cards, grouped[0].Face));
    }

    if (isFlush)
    {
      var flushSuit = cards.First().Suit.GetDisplayName();
      return CreateResult(Rank.Flush, flushSuit, cards,
        BuildRankValuesFromFaces(cards.OrderByDescending(card => card.Face).Select(card => card.Face).ToList(), false),
        cards.OrderByDescending(card => card.Face).First());
    }

    if (isStraight)
    {
      return CreateResult(Rank.Straight, straightHighFace.GetDisplayName(), cards,
        BuildRankValuesFromFaces(straightOrder, isWheel), GetCardForFace(cards, straightHighFace));
    }

    if (grouped[0].Count == 3)
    {
      return CreateResult(Rank.Set, grouped[0].Face.GetDisplayName(), cards, BuildRankValues(grouped, false),
        GetCardForFace(cards, grouped[0].Face));
    }

    if (grouped.Length == 3)
    {
      var combo = $"{grouped[0].Face.GetDisplayName()},{grouped[1].Face.GetDisplayName()}";
      return CreateResult(Rank.TwoPairs, combo, cards, BuildRankValues(grouped, false),
        GetCardForFace(cards, grouped[0].Face));
    }

    if (grouped[0].Count == 2)
    {
      return CreateResult(Rank.OnePairs, grouped[0].Face.GetDisplayName(), cards, BuildRankValues(grouped, false),
        GetCardForFace(cards, grouped[0].Face));
    }

    return CreateResult(Rank.HighCard, cards.MaxBy(card => card.Face)!.FaceName, cards,
      BuildRankValuesFromFaces(cards.OrderByDescending(card => card.Face).Select(card => card.Face).ToList(), false),
      cards.MaxBy(card => card.Face)!);
  }

  private static EvaluationResult CreateResult(Rank rank, string combo, List<Card> cards, List<int> rankValues,
    Card highCard)
  {
    return new EvaluationResult(rank, combo, cards, rankValues, highCard);
  }

  private EvaluationResult SelectBetterResult(EvaluationResult? current, EvaluationResult challenger)
  {
    if (current == null)
    {
      return challenger;
    }

    if (current.Rank != challenger.Rank)
    {
      return current.Rank > challenger.Rank ? current : challenger;
    }

    var maxLength = Math.Max(current.RankValues.Count, challenger.RankValues.Count);

    for (var i = 0; i < maxLength; i++)
    {
      var left = i < current.RankValues.Count ? current.RankValues[i] : 0;
      var right = i < challenger.RankValues.Count ? challenger.RankValues[i] : 0;

      if (left == right)
      {
        continue;
      }

      return left > right ? current : challenger;
    }

    return challenger;
  }

  private static bool IsFlush(List<Card> cards)
  {
    var firstSuit = cards.First().Suit;
    return cards.All(card => card.Suit == firstSuit);
  }

  private static bool IsStraight(List<Face> faces, out Face highFace, out List<Face> orderedFaces, out bool isWheel)
  {
    var ordered = faces.OrderBy(face => face).ToArray();
    isWheel = ordered.SequenceEqual(new[] { Face.Two, Face.Three, Face.Four, Face.Five, Face.Ace });

    if (isWheel)
    {
      highFace = Face.Five;
      orderedFaces = new List<Face> { Face.Five, Face.Four, Face.Three, Face.Two, Face.Ace };
      return true;
    }

    for (var index = 0; index < ordered.Length - 1; index++)
    {
      if (ordered[index] + 1 != ordered[index + 1])
      {
        highFace = Face.Unknown;
        orderedFaces = new List<Face>();
        return false;
      }
    }

    highFace = ordered.Last();
    orderedFaces = ordered.OrderByDescending(face => face).ToList();
    return true;
  }

  private static Card GetCardForFace(IEnumerable<Card> cards, Face face)
  {
    return cards.Last(card => card.Face == face);
  }

  private static List<int> BuildRankValues(IEnumerable<(Face Face, int Count)> grouped, bool aceLow)
  {
    var values = new List<int>();

    foreach (var group in grouped)
    {
      var value = GetFaceValue(group.Face, aceLow);
      for (var i = 0; i < group.Count; i++)
      {
        values.Add(value);
      }
    }

    return values;
  }

  private static List<int> BuildRankValuesFromFaces(List<Face> faces, bool aceLow)
  {
    return faces.Select(face => GetFaceValue(face, aceLow)).ToList();
  }

  private static int GetFaceValue(Face face, bool aceLow)
  {
    return face == Face.Ace && aceLow
      ? 1
      : (int)face + 2;
  }

  private static Face[][] GetStraightSequences()
  {
    return new[]
    {
      new[] { Face.Ace, Face.Two, Face.Three, Face.Four, Face.Five },
      new[] { Face.Two, Face.Three, Face.Four, Face.Five, Face.Six },
      new[] { Face.Three, Face.Four, Face.Five, Face.Six, Face.Seven },
      new[] { Face.Four, Face.Five, Face.Six, Face.Seven, Face.Eight },
      new[] { Face.Five, Face.Six, Face.Seven, Face.Eight, Face.Nine },
      new[] { Face.Six, Face.Seven, Face.Eight, Face.Nine, Face.Ten },
      new[] { Face.Seven, Face.Eight, Face.Nine, Face.Ten, Face.Jack },
      new[] { Face.Eight, Face.Nine, Face.Ten, Face.Jack, Face.Queen },
      new[] { Face.Nine, Face.Ten, Face.Jack, Face.Queen, Face.King },
      new[] { Face.Ten, Face.Jack, Face.Queen, Face.King, Face.Ace }
    };
  }

  private record EvaluationResult(Rank Rank, string Combo, List<Card> Cards, List<int> RankValues, Card HighCard);
}
