using CardDeck.Core.Base;
using Poker.Interfaces;

namespace Poker.Implementation
{
  public class PokerDeck : Deck, IPokerDeck
  {
    public Deal DealCard()
    {
      while (true)
      {
        var deal = new Deal();
        if (CurrentCard + 5 < CardDeck.Length)
        {
          for (var i = 0; i < 5; i++)
          {
            deal.AddCard(CardDeck[CurrentCard++]);
          }

          deal.Check();

          return deal;
        }
      
        Shuffle();
      }
    }
  }
}