using System.ComponentModel.DataAnnotations;

namespace Poker.Enums;

public enum Rank
{
  [Display(Name = "Сдали что-то не то")]
  Unknown,
  [Display(Name = "Старшая карта")]
  HighCard,
  [Display(Name = "Одна пара")]
  OnePairs,
  [Display(Name = "Две пары")]
  TwoPairs,
  [Display(Name = "Сет")]
  Set,
  [Display(Name = "Стрит")]
  Straight,
  [Display(Name = "Флеш")]
  Flush,
  [Display(Name = "Фулл-хаус")]
  FullHouse,
  [Display(Name = "Каре")]
  Quads,
  [Display(Name = "Стрит-флеш")]
  StraightFlush,
  [Display(Name = "Флеш-рояль")]
  RoyalFlush,
}