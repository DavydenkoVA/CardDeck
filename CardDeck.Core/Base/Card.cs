using CardDeck.Core.Enums;

namespace CardDeck.Core.Base;

public class Card
{
  public Face Face { get; }
  public Suit Suit { get; }

  public Card(Face cardFace, Suit cardSuit)
  {
    Face = cardFace;
    Suit = cardSuit;
  }

  public override string ToString()
  {
    return $"{Face.GetDisplayName()}{Suit.GetDisplayName()}";
  }
}