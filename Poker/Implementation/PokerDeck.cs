using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Poker.Dtos;
using Poker.Interfaces;

namespace Poker.Implementation
{
  public class PokerDeck : Deck, IDeck
  {
    public ActionResult DealCard()
    {
      while (true)
      {
        if (_currentCard + 5 < _deck.Length)
        {
          var deal = new List<Card>
          {
            _deck[_currentCard++],
            _deck[_currentCard++],
            _deck[_currentCard++],
            _deck[_currentCard++],
            _deck[_currentCard++]
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