using Microsoft.AspNetCore.Mvc;

namespace CardDeck.Interfaces;

public interface IDeck
{
  ActionResult DealCard();
}