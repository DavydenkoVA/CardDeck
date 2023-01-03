using Microsoft.AspNetCore.Mvc;

namespace Poker.Interfaces;

public interface IDeck
{
  ActionResult DealCard();
}