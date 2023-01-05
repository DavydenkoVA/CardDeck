using CardDeck.Core.Enums;

namespace CardDeck.Core.Base;

public class Card
{
  public Face Face { get; }
  public Suit Suit { get; }
  public string FaceName => Face.GetDisplayName();
  public string SuitName => Suit.GetDisplayName();
  public string CardName => ToString();

  public Card(Face cardFace, Suit cardSuit)
  {
    Face = cardFace;
    Suit = cardSuit;
  }

  public override string ToString()
  {
    return $"{FaceName}{SuitName}";
  }
}