using Microsoft.AspNetCore.Mvc;
using Poker.Interfaces;
using ViennaNET.Utils;

namespace CardDeck;

[Route("api/[controller]")]
public class PokerController : Controller
{
  private readonly IDeck _deck;

  public PokerController(IDeck deck)
  {
    _deck = deck.ThrowIfNull(nameof(deck));
  }
  
  [HttpGet("DealCard")]
  public IActionResult DealCard()
  {
    var result = _deck.DealCard();
    return Ok(result);
  }
}