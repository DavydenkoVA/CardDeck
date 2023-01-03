using CardDeck.Core.Enums;

namespace CardDeck.Core.Base
{
  public abstract class Deck
  {
    private const short NumberOfCards = 52;
    protected readonly Card[] _deck;
    protected int _currentCard;
    private readonly Random _rnd;

    protected Deck()
    {

      _deck = new Card[NumberOfCards];
      _currentCard = 0;
      _rnd = new Random();
      for (var count = 0; count < _deck.Length; count++)
      {
        _deck[count] = new Card((Face)(count % 13), (Suit)(count / 13));
      }
      Shuffle();
    }

    protected void Shuffle()
    {
      _currentCard = 0;
      for (var first = 0; first < _deck.Length; first++)
      {
        var second = _rnd.Next(NumberOfCards);
        (_deck[first], _deck[second]) = (_deck[second], _deck[first]);
      }
    }

  }
}