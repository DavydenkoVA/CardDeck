using System.Collections.Generic;
using System.Linq;
using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using Poker.Enums;

namespace Poker.Implementation;

public static class RankChecker
{
  public static Rank Check(List<Card> cards)
  {
    if (cards.Count != 5) return Rank.Unknown;

    var flush = CheckFlush(cards);
    var straight = CheckStraight(cards);

    if (flush && straight)
    {
      return cards.Min(c => c.Face) == Face.Ten ? Rank.RoyalFlush : Rank.StraightFlush;
    }

    var groups = cards
      .GroupBy(c => c.Face)
      .Select(g => g.Count())
      .OrderByDescending(x => x)
      .ToArray();

    if (groups.Length == 2)
    {
      return groups[0] == 4 ? Rank.Quads : Rank.FullHouse;
    }

    if (flush) return Rank.Flush;

    if (straight) return Rank.Straight;

    if (groups[0] == 3) return Rank.Set;

    if (groups.Length == 3) return Rank.TwoPairs;
    
    return groups[0] == 2 ? Rank.OnePairs : Rank.HighCard;
  }

  public static bool CheckFlush(IEnumerable<Card> cards)
  {
    var check = cards.GroupBy(c => c.Suit);
    return check.Count() == 1;
  }

  public static bool CheckStraight(IList<Card> cards)
  {
    var check = cards.Select(c => c.Face).OrderBy(face => face).ToArray();
    for (var i = 0; i < cards.Count - 1; i++)
    {
      if (check[i] + 1 == check[i + 1]) continue;
      
      return i == cards.Count - 2 && check[i + 1] == Face.Ace && check[i] == Face.Five;
    }

    return true;
  }
}
