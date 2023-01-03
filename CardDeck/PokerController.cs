using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Poker.Interfaces;
using ViennaNET.Utils;

namespace CardDeck;

/// <summary>
/// Игра в покер
/// </summary>
[ExcludeFromCodeCoverage]
[Route("api/[controller]")]
public class PokerController : Controller
{
  private readonly IPokerDeck _deck;

  public PokerController(IPokerDeck deck)
  {
    _deck = deck.ThrowIfNull(nameof(deck));
  }
  
  /// <summary>
  /// Сдача карт
  /// </summary>
  /// <remarks>Сдача карт игрокам</remarks>
  [HttpGet("DealCard")]
  public IActionResult DealCard()
  {
    var result = _deck.DealCard();
    return Ok(result);
  }
}