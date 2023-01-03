using System.ComponentModel.DataAnnotations;

namespace CardDeck.Enums;

public enum Face
{
  [Display(Name = "2")]
  Two,
  [Display(Name = "3")]
  Three,
  [Display(Name = "4")]
  Four,
  [Display(Name = "5")]
  Five,
  [Display(Name = "6")]
  Six,
  [Display(Name = "7")]
  Seven,
  [Display(Name = "8")]
  Eight,
  [Display(Name = "9")]
  Nine,
  [Display(Name = "10")]
  Ten,
  [Display(Name = "В")]
  Jack,
  [Display(Name = "Д")]
  Queen,
  [Display(Name = "К")]
  King,
  [Display(Name = "Т")]
  Ace,
}