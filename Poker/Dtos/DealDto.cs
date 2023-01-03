using Microsoft.AspNetCore.Mvc;
using Poker.Enums;

namespace Poker.Dtos;

public class DealDto : ActionResult
{
  public string Cards { get; set; }
  public Rank Rank { get; set; }
  public string RankName { get; set; } 
  public string RankDescription { get; set; } 
}