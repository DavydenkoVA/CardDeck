using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using NUnit.Framework;
using Poker.Enums;
using Poker.Implementation;

namespace Poker.Test;

public class DealRankStraightTests
{
  [Test]
  public void CheckRankStraightHighAce_Pass()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    deal.AddCard(new Card(Face.King, Suit.Hearts));
    deal.AddCard(new Card(Face.Jack, Suit.Clubs));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Queen, Suit.Clubs));
    
    deal.Check();
    
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.Straight));
      Assert.That(deal.RankDescription, Is.EqualTo(Rank.Straight.GetDisplayName()));
      Assert.That(deal.RankName, Is.EqualTo(nameof(Rank.Straight)));
      Assert.That(deal.RankCombo, Is.EqualTo(Face.Ace.GetDisplayName()));
    });
  }
  
  [Test]
  public void CheckRankStraightLowAce_Pass()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Five, Suit.Clubs));
    deal.AddCard(new Card(Face.Two, Suit.Hearts));
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    deal.AddCard(new Card(Face.Four, Suit.Clubs));
    deal.AddCard(new Card(Face.Three, Suit.Clubs));
    
    deal.Check();
    
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.Straight));
      Assert.That(deal.RankDescription, Is.EqualTo(Rank.Straight.GetDisplayName()));
      Assert.That(deal.RankName, Is.EqualTo(nameof(Rank.Straight)));
      Assert.That(deal.RankCombo, Is.EqualTo(Face.Five.GetDisplayName()));
    });
  }
  
  [Test]
  public void CheckRankStraight_Pass()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Nine, Suit.Clubs));
    deal.AddCard(new Card(Face.King, Suit.Hearts));
    deal.AddCard(new Card(Face.Jack, Suit.Clubs));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Queen, Suit.Clubs));
    
    deal.Check();
    
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.Straight));
      Assert.That(deal.RankDescription, Is.EqualTo(Rank.Straight.GetDisplayName()));
      Assert.That(deal.RankName, Is.EqualTo(nameof(Rank.Straight)));
      Assert.That(deal.RankCombo, Is.EqualTo(Face.King.GetDisplayName()));
    });
  }
  
  [Test]
  public void CheckRankStraight_WithJoker()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Nine, Suit.Clubs));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    deal.AddCard(new Card(Face.Jack, Suit.Spades));
    deal.AddCard(new Card(Face.Ten, Suit.Hearts));
    deal.AddCard(new Card(Face.Seven, Suit.Clubs));
    
    deal.Check();
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.Straight));
      Assert.That(deal.RankCombo, Is.EqualTo(Face.Jack.GetDisplayName()));
    });
  }

  [Test]
  public void CheckRankStraight_WithJokerCombo()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Nine, Suit.Clubs));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    deal.AddCard(new Card(Face.Jack, Suit.Spades));
    deal.AddCard(new Card(Face.Ten, Suit.Hearts));
    deal.AddCard(new Card(Face.Eight, Suit.Clubs));
    
    deal.Check();
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.Straight));
      Assert.That(deal.RankCombo, Is.EqualTo(Face.Queen.GetDisplayName()));
    });
  }

  [Test]
  public void CheckRankStraight_WithTwoJoker()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Nine, Suit.Clubs));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    deal.AddCard(new Card(Face.Jack, Suit.Spades));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    deal.AddCard(new Card(Face.Eight, Suit.Clubs));
    
    deal.Check();
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.Straight));
      Assert.That(deal.RankCombo, Is.EqualTo(Face.Queen.GetDisplayName()));
    });
  }
}