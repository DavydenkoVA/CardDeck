using System.ComponentModel.DataAnnotations;

namespace CardDeck.Core.Enums;

public enum Suit
{
  [Display(Name = "♣")]
  Clubs,
  [Display(Name = "♥")]
  Hearts,
  [Display(Name = "♦")]
  Diamonds,
  [Display(Name = "♠")]
  Spades,
  [Display(Name = "🎭")]
  Jocker,
}