using System.Text.Json.Serialization;
using CardDeck.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CardDeck.Dtos;

public class DealDtp : ActionResult
{
  public string Cards { get; set; }
  public Rank Rank { get; set; }
  public string RankName { get; set; } 
  public string RankDescription { get; set; } 
}