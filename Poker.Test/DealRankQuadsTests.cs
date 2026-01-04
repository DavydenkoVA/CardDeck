using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using NUnit.Framework;
using Poker.Enums;
using Poker.Implementation;

namespace Poker.Test;

public class DealRankQuadsTests
{
  [Test]
  public void CheckRankQuads_Pass()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Queen, Suit.Hearts));
    deal.AddCard(new Card(Face.Queen, Suit.Diamonds));
    deal.AddCard(new Card(Face.Queen, Suit.Spades));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Queen, Suit.Clubs));
    
    deal.Check();
    
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.Quads));
      Assert.That(deal.RankDescription, Is.EqualTo(Rank.Quads.GetDisplayName()));
      Assert.That(deal.RankName, Is.EqualTo(nameof(Rank.Quads)));
      Assert.That(deal.RankCombo, Is.EqualTo(Face.Queen.GetDisplayName()));
    });
  }
  
  [Test]
  public void CheckRankQuads_WithJoker()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Queen, Suit.Hearts));
    deal.AddCard(new Card(Face.Queen, Suit.Diamonds));
    deal.AddCard(new Card(Face.Queen, Suit.Spades));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    
    deal.Check();
    Assert.Multiple(() => 
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.Quads));
      Assert.That(deal.RankCombo, Is.EqualTo(Face.Queen.GetDisplayName())); 
    });
  }
  
  [Test]
  public void CheckRankQuads_WithTwoJoker()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Queen, Suit.Hearts));
    deal.AddCard(new Card(Face.Queen, Suit.Diamonds));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    
    deal.Check();
    Assert.Multiple(() => 
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.Quads));
      Assert.That(deal.RankCombo, Is.EqualTo(Face.Queen.GetDisplayName())); 
    });
  }
}
