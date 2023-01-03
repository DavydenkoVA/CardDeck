using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using Poker.Dtos;
using Poker.Interfaces;

namespace Poker.Implementation
{
  public class PokerDeck : Deck, IPokerDeck
  {
    public DealDto DealCard()
    {
      while (true)
      {
        if (CurrentCard + 5 < CardDeck.Length)
        {
          var deal = new List<Card>
          {
            CardDeck[CurrentCard++],
            CardDeck[CurrentCard++],
            CardDeck[CurrentCard++],
            CardDeck[CurrentCard++],
            CardDeck[CurrentCard++]
          };
          var rank = RankChecker.Check(deal);

          var result = new DealDto()
          {
            Cards = "|" + string.Join('|', deal) + "|",
            Rank = rank, 
            RankName = rank.ToString(),
            RankDescription = rank.GetDisplayName(),
          };

          return result;
        }
      
        Shuffle();
      }
    }
  }
}