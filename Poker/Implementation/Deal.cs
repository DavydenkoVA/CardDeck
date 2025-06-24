using System.Net.Mime;
using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using Poker.Enums;

namespace Poker.Implementation;

public class Deal
{
  private List<Card> _cards { get; }
  //public List<Card> DealCards => _cards;
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

    var jokerCount = _cards.Count(c => c.Face == Face.Joker);
    if (jokerCount == 0)
    {
      (Rank, RankCombo) = Evaluate(_cards);
      return;
    }

    var nonJokers = _cards.Where(c => c.Face != Face.Joker).ToList();
    var suits = Enum.GetValues(typeof(Suit)).Cast<Suit>().Where(s => s != Suit.Joker).ToArray();
    var faces = Enum.GetValues(typeof(Face)).Cast<Face>().Where(f => f != Face.Joker).ToArray();

    Rank bestRank = Rank.Unknown;
    string bestCombo = string.Empty;

    void EvaluateCandidate(List<Card> candidate)
    {
      var (r, c) = Evaluate(candidate);
      if (r > bestRank)
      {
        bestRank = r;
        bestCombo = c;
      }
    }

    if (jokerCount == 1)
    {
      foreach (var f in faces)
      foreach (var s in suits)
        EvaluateCandidate(new List<Card>(nonJokers) { new Card(f, s) });
    }
    else if (jokerCount == 2)
    {
      foreach (var f1 in faces)
      foreach (var s1 in suits)
      foreach (var f2 in faces)
      foreach (var s2 in suits)
        EvaluateCandidate(new List<Card>(nonJokers) { new Card(f1, s1), new Card(f2, s2) });
    }

    Rank = bestRank;
    RankCombo = bestCombo;
  }

  private static (Rank rank, string combo) Evaluate(List<Card> cards)
  {
    var flush = CheckFlush(cards);
    var straight = CheckStraight(cards);

    if (flush && straight)
    {
      var rank = cards.Min(c => c.Face) == Face.Ten ? Rank.RoyalFlush : Rank.StraightFlush;
      return (rank, GetHighCard(cards).ToString());
    }

    var groups = cards
      .GroupBy(c => c.Face)
      .Select(g => new { Face = g.Key, Count = g.Count() })
      .OrderByDescending(x => x.Count)
      .ThenByDescending(x => x.Face)
      .ToArray();

    if (groups.Length == 2)
    {
      var rank = groups[0].Count == 4 ? Rank.Quads : Rank.FullHouse;
      var combo = groups[0].Face.GetDisplayName() + (groups[0].Count == 4 ? string.Empty : $",{groups[1].Face.GetDisplayName()}");
      return (rank, combo);
    }

    if (flush)
    {
      return (Rank.Flush, cards.Select(c => c.Suit.GetDisplayName()).FirstOrDefault() ?? string.Empty);
    }

    if (straight)
    {
      return (Rank.Straight, GetHighCard(cards).FaceName);
    }

    if (groups[0].Count == 3)
    {
      return (Rank.Set, groups[0].Face.GetDisplayName());
    }

    if (groups.Length == 3)
    {
      return (Rank.TwoPairs, $"{groups[0].Face.GetDisplayName()},{groups[1].Face.GetDisplayName()}");
    }

    if (groups[0].Count == 2)
    {
      return (Rank.OnePairs, groups[0].Face.GetDisplayName());
    }

    return (Rank.HighCard, GetHighCard(cards).FaceName);
  }

  private static bool CheckFlush(List<Card> cards)
  {
    var check = cards.GroupBy(c => c.Suit);
    return check.Count() == 1;
  }

  private static bool CheckStraight(List<Card> cards)
  {
    var check = cards.Select(c => c.Face).OrderBy(face => face).ToArray();
    for (var i = 0; i < cards.Count - 1; i++)
    {
      if (check[i] + 1 == check[i + 1]) continue;

      return i == cards.Count - 2 && check[i + 1] == Face.Ace && check[i] == Face.Five;
    }

    return true;
  }

  private static Card GetHighCard(List<Card> cards)
  {
    var find = cards.OrderBy(card => card.Face).ToArray();

    return CheckStraight(cards) && find[0].Face == Face.Two && find[4].Face == Face.Ace
      ? find[3]
      : find[4];
  }

  public bool CheckFlush()
  {
    var check = _cards.GroupBy(c => c.Suit);
    return check.Count() == 1;
  }

  public bool CheckStraight()
  {
    var check = _cards.Select(c => c.Face).OrderBy(face => face).ToArray();
    for (var i = 0; i < _cards.Count - 1; i++)
    {
      if (check[i] + 1 == check[i + 1]) continue;

      return i == _cards.Count - 2 && check[i + 1] == Face.Ace && check[i] == Face.Five;
    }

    return true;
  }

  public Card GetHighCard()
  {
    var find = _cards.OrderBy(card => card.Face).ToArray();

    return CheckStraight() && find[0].Face == Face.Two && find[4].Face == Face.Ace
      ? find[3]
      : find[4];
  }
}