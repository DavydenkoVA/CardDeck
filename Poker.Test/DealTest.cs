using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using NUnit.Framework;
using Poker.Enums;
using Poker.Implementation;

namespace Poker.Test;

public class DealTest
{
  [SetUp]
  public void Setup()
  {
  }

  [Test]
  public void GetCards_Pass()
  {
    var deal = new Deal();

    var card1 = new Card(Face.Ace, Suit.Clubs);
    var card2 = new Card(Face.Ten, Suit.Diamonds);
    
    deal.AddCard(card1);
    deal.AddCard(card2);

    Assert.That(deal.Cards, Is.EqualTo($"│{card1.CardName}│{card2.CardName}│"));
  }

  [Test]
  public void CheckFlush_Pass()
  {
    var deal = new Deal();

    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));

    Assert.That(deal.CheckFlush(), Is.True);
  }
  
  [Test]
  public void CheckFlush_NotPass()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    deal.AddCard(new Card(Face.Ace, Suit.Diamonds));

    Assert.That(deal.CheckFlush(), Is.False);
  }
  
  [Test]
  public void CheckStraight_Pass()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Six, Suit.Clubs));
    deal.AddCard(new Card(Face.Two, Suit.Clubs));
    deal.AddCard(new Card(Face.Three, Suit.Clubs));
    deal.AddCard(new Card(Face.Five, Suit.Clubs));
    deal.AddCard(new Card(Face.Four, Suit.Clubs));
    
    Assert.That(deal.CheckStraight(), Is.True);
  }
  
  [Test]
  public void CheckStraight_NotPass()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    deal.AddCard(new Card(Face.King, Suit.Clubs));
    deal.AddCard(new Card(Face.Queen, Suit.Diamonds));
    
    Assert.That(deal.CheckStraight(), Is.False);
  }
  
  [Test]
  public void CheckStraightLowAce_Pass()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Four, Suit.Clubs));
    deal.AddCard(new Card(Face.Two, Suit.Clubs));
    deal.AddCard(new Card(Face.Three, Suit.Clubs));
    deal.AddCard(new Card(Face.Five, Suit.Clubs));
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    
    Assert.That(deal.CheckStraight(), Is.True);
  }
  
  [Test]
  public void CheckStraightHighAce_Pass()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.King, Suit.Clubs));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Queen, Suit.Clubs));
    deal.AddCard(new Card(Face.Jack, Suit.Clubs));
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    
    Assert.That(deal.CheckStraight(), Is.True);
  }
  
  [Test]
  public void CheckStraightAce_NotPass()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Four, Suit.Clubs));
    deal.AddCard(new Card(Face.Two, Suit.Clubs));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Five, Suit.Clubs));
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    
    Assert.That(deal.CheckStraight(), Is.False);
  }
  
  [Test]
  public void CheckRankUnknown_Pass()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    
    deal.Check();
    
    Assert.Multiple(() =>
      {
        Assert.That(deal.Rank, Is.EqualTo(Rank.Unknown));
        Assert.That(deal.RankDescription, Is.EqualTo(Rank.Unknown.GetDisplayName()));
        Assert.That(deal.RankName, Is.EqualTo(Rank.Unknown.ToString()));
        Assert.That(deal.RankCombo, Is.EqualTo(string.Empty));
      });
  }
  
  [Test]
  public void CheckRankRoyalFlush_Pass()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    deal.AddCard(new Card(Face.King, Suit.Clubs));
    deal.AddCard(new Card(Face.Jack, Suit.Clubs));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Queen, Suit.Clubs));
    
    deal.Check();
    
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.RoyalFlush));
      Assert.That(deal.RankDescription, Is.EqualTo(Rank.RoyalFlush.GetDisplayName()));
      Assert.That(deal.RankName, Is.EqualTo(Rank.RoyalFlush.ToString()));
      Assert.That(deal.RankCombo, Is.EqualTo($"{Face.Ace.GetDisplayName()}{Suit.Clubs.GetDisplayName()}"));
    });
  }
  
  [Test]
  public void CheckRankStraightFlush_Pass()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Nine, Suit.Clubs));
    deal.AddCard(new Card(Face.King, Suit.Clubs));
    deal.AddCard(new Card(Face.Jack, Suit.Clubs));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Queen, Suit.Clubs));
    
    deal.Check();
    
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.StraightFlush));
      Assert.That(deal.RankDescription, Is.EqualTo(Rank.StraightFlush.GetDisplayName()));
      Assert.That(deal.RankName, Is.EqualTo(Rank.StraightFlush.ToString()));
      Assert.That(deal.RankCombo, Is.EqualTo($"{Face.King.GetDisplayName()}{Suit.Clubs.GetDisplayName()}"));
    });
  }
  
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
      Assert.That(deal.RankName, Is.EqualTo(Rank.Quads.ToString()));
      Assert.That(deal.RankCombo, Is.EqualTo(Face.Queen.GetDisplayName()));
    });
  }
  
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
      Assert.That(deal.RankName, Is.EqualTo(Rank.FullHouse.ToString()));
      Assert.That(deal.RankCombo, Is.EqualTo($"{Face.Queen.GetDisplayName()},{Face.Ten.GetDisplayName()}"));
    });
  }
  
  [Test]
  public void CheckRankFlush_Pass()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Nine, Suit.Clubs));
    deal.AddCard(new Card(Face.King, Suit.Clubs));
    deal.AddCard(new Card(Face.Two, Suit.Clubs));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Queen, Suit.Clubs));
    
    deal.Check();
    
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.Flush));
      Assert.That(deal.RankDescription, Is.EqualTo(Rank.Flush.GetDisplayName()));
      Assert.That(deal.RankName, Is.EqualTo(Rank.Flush.ToString()));
      Assert.That(deal.RankCombo, Is.EqualTo(Suit.Clubs.GetDisplayName()));
    });
  }
  
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
      Assert.That(deal.RankName, Is.EqualTo(Rank.Straight.ToString()));
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
      Assert.That(deal.RankName, Is.EqualTo(Rank.Straight.ToString()));
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
      Assert.That(deal.RankName, Is.EqualTo(Rank.Straight.ToString()));
      Assert.That(deal.RankCombo, Is.EqualTo(Face.King.GetDisplayName()));
    });
  }
  
  [Test]
  public void CheckRankSet_Pass()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Queen, Suit.Hearts));
    deal.AddCard(new Card(Face.Queen, Suit.Diamonds));
    deal.AddCard(new Card(Face.Five, Suit.Spades));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Queen, Suit.Clubs));
    
    deal.Check();
    
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.Set));
      Assert.That(deal.RankDescription, Is.EqualTo(Rank.Set.GetDisplayName()));
      Assert.That(deal.RankName, Is.EqualTo(Rank.Set.ToString()));
      Assert.That(deal.RankCombo, Is.EqualTo(Face.Queen.GetDisplayName()));
    });
  }
  
  [Test]
  public void CheckRankTwoPairs_Pass()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Queen, Suit.Hearts));
    deal.AddCard(new Card(Face.King, Suit.Diamonds));
    deal.AddCard(new Card(Face.Ten, Suit.Spades));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Queen, Suit.Clubs));
    
    deal.Check();
    
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.TwoPairs));
      Assert.That(deal.RankDescription, Is.EqualTo(Rank.TwoPairs.GetDisplayName()));
      Assert.That(deal.RankName, Is.EqualTo(Rank.TwoPairs.ToString()));
      Assert.That(deal.RankCombo, Is.EqualTo($"{Face.Queen.GetDisplayName()},{Face.Ten.GetDisplayName()}"));
    });
  }
  
  [Test]
  public void CheckRankOnePairs_Pass()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Queen, Suit.Hearts));
    deal.AddCard(new Card(Face.King, Suit.Diamonds));
    deal.AddCard(new Card(Face.Jack, Suit.Spades));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Queen, Suit.Clubs));
    
    deal.Check();
    
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.OnePairs));
      Assert.That(deal.RankDescription, Is.EqualTo(Rank.OnePairs.GetDisplayName()));
      Assert.That(deal.RankName, Is.EqualTo(Rank.OnePairs.ToString()));
      Assert.That(deal.RankCombo, Is.EqualTo(Face.Queen.GetDisplayName()));
    });
  }
  
  [Test]
  public void CheckRankHighCard_Pass()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Queen, Suit.Hearts));
    deal.AddCard(new Card(Face.King, Suit.Diamonds));
    deal.AddCard(new Card(Face.Jack, Suit.Spades));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Eight, Suit.Clubs));
    
    deal.Check();
    
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.HighCard));
      Assert.That(deal.RankDescription, Is.EqualTo(Rank.HighCard.GetDisplayName()));
      Assert.That(deal.RankName, Is.EqualTo(Rank.HighCard.ToString()));
      Assert.That(deal.RankCombo, Is.EqualTo(Face.King.GetDisplayName()));
    });
  }
}