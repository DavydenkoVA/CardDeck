using Poker.Dtos;

namespace Poker.Interfaces;

public interface IPokerDeck
{
  DealDto DealCard();
}