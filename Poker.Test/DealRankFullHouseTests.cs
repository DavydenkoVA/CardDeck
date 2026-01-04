using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using NUnit.Framework;
using Poker.Enums;
using Poker.Implementation;

namespace Poker.Test;

public class DealRankFullHouseTests
{
  [Test]
  public void CheckRankFullHouse_Pass()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Queen, Suit.Hearts));
    deal.AddCard(new Card(Face.Queen, Suit.Diamonds));
    deal.AddCard(new Card(Face.Ten, Suit.Spades));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Queen, Suit.Clubs));
    
    deal.Check();
    
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.FullHouse));
      Assert.That(deal.RankDescription, Is.EqualTo(Rank.FullHouse.GetDisplayName()));
      Assert.That(deal.RankName, Is.EqualTo(nameof(Rank.FullHouse)));
      Assert.That(deal.RankCombo, Is.EqualTo($"{Face.Queen.GetDisplayName()},{Face.Ten.GetDisplayName()}"));
    });
  }
  
  [Test]
  public void CheckRankFullHouse_WithJoker()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Queen, Suit.Hearts));
    deal.AddCard(new Card(Face.Queen, Suit.Diamonds));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Ten, Suit.Spades));
    
    deal.Check();
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.FullHouse));
      Assert.That(deal.RankCombo, Is.EqualTo($"{Face.Queen.GetDisplayName()},{Face.Ten.GetDisplayName()}"));
    });
  }
  
  [Test]
  public void CheckRankFullHouse_WithTwoJoker()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Queen, Suit.Hearts));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Ten, Suit.Spades));
    
    deal.Check();
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, !Is.EqualTo(Rank.FullHouse));
    });
  }
}
