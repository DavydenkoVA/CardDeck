using CardDeck.Core.Enums;

namespace CardDeck.Core.Base
{
  public abstract class Deck
  {
    private const short NumberOfCards = 54;
    protected readonly Card[] CardDeck;
    protected int CurrentCard;
    private readonly Random _rnd;

    protected Deck()
    {
      CardDeck = new Card[NumberOfCards];
      CurrentCard = 0;
      _rnd = new Random();
      for (var count = 0; count < 52; count++)
      {
        CardDeck[count] = new Card((Face)(count % 13), (Suit)(count / 13));
      }
      CardDeck[52] = new Card(Face.Joker, Suit.Joker);
      CardDeck[53] = new Card(Face.Joker, Suit.Joker);
      Shuffle();
    }

    protected void Shuffle()
    {
      CurrentCard = 0;
      for (var first = 0; first < CardDeck.Length; first++)
      {
        var second = _rnd.Next(NumberOfCards);
        (CardDeck[first], CardDeck[second]) = (CardDeck[second], CardDeck[first]);
      }
    }

  }
}