using System.Net.Mime;
using System.Threading;
using CardDeck.Implementation;
using CardDeck.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ViennaNET.Utils;

namespace CardDeck;

[Route("api/[controller]")]
public class DeckController : Controller
{
  private readonly IDeck _deck;

  public DeckController(IDeck deck)
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