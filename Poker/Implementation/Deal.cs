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
    return "|" + string.Join('|', _cards) + "|";
  }

  public void Check()
  {
    if (_cards.Count != 5)
    {
      Rank = Rank.Unknown;
      RankCombo = string.Empty;
      return;
    }

    var flush = CheckFlush();
    var straight = CheckStraight();

    if (flush && straight)
    {
      Rank = _cards.Min(c => c.Face) == Face.Ten ? Rank.RoyalFlush : Rank.StraightFlush;
      RankCombo = GetHighCard().ToString();
      return;
    }

    var groups = _cards
      .GroupBy(c => c.Face)
      .Select(g => new {Face = g.Key, Count = g.Count()})
      .OrderByDescending(x => x.Count)
      .ThenByDescending(x => x.Face)
      .ToArray();

    if (groups.Length == 2)
    {
      Rank = groups[0].Count == 4 ? Rank.Quads : Rank.FullHouse;
      RankCombo = groups[0].Face.GetDisplayName() + (groups[0].Count == 4 ? string.Empty : $",{groups[1].Face.GetDisplayName()}");
      return;
    }

    if (flush)
    {
      Rank = Rank.Flush;
      RankCombo = _cards.Select(c => c.Suit.GetDisplayName()).FirstOrDefault() ?? string.Empty;
      return;
    }

    if (straight)
    {
      Rank = Rank.Straight;
      RankCombo = GetHighCard().FaceName;
      return;
    }

    if (groups[0].Count == 3)
    {
      Rank = Rank.Set;
      RankCombo = groups[0].Face.GetDisplayName();
      return;
    }

    if (groups.Length == 3)
    {
      Rank = Rank.TwoPairs;
      RankCombo = $"{groups[0].Face.GetDisplayName()},{groups[1].Face.GetDisplayName()}";
      return;
    }

    if (groups[0].Count == 2)
    {
      Rank = Rank.OnePairs;
      RankCombo = groups[0].Face.GetDisplayName();
      return;
    }

    Rank = Rank.HighCard;
    RankCombo = GetHighCard().FaceName;
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