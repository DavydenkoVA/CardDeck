using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using NUnit.Framework;
using Poker.Enums;
using Poker.Implementation;

namespace Poker.Test;

public class RankCheckerTest
{
  [SetUp]
  public void Setup()
  {
  }

  [Test]
  public void CheckFlush_Pass()
  {
    var deal = new List<Card>
    {
      new(Face.Ace, Suit.Clubs),
      new(Face.Ace, Suit.Clubs),
      new(Face.Ace, Suit.Clubs),
      new(Face.Ace, Suit.Clubs),
      new(Face.Ace, Suit.Clubs)
    };

    Assert.That(RankChecker.CheckFlush(deal), Is.True);
  }
  
  [Test]
  public void CheckFlush_NotPass()
  {
    var deal = new List<Card>
    {
      new(Face.Ace, Suit.Clubs),
      new(Face.Ace, Suit.Clubs),
      new(Face.Ace, Suit.Clubs),
      new(Face.Ace, Suit.Clubs),
      new(Face.Ace, Suit.Diamonds)
    };

    Assert.That(RankChecker.CheckFlush(deal), Is.False);
  }
  
  [Test]
  public void CheckStraight_Pass()
  {
    var deal = new List<Card>
    {
      new(Face.Six, Suit.Clubs),
      new(Face.Two, Suit.Clubs),
      new(Face.Three, Suit.Clubs),
      new(Face.Five, Suit.Clubs),
      new(Face.Four, Suit.Clubs)
    };

    Assert.That(RankChecker.CheckStraight(deal), Is.True);
  }
  
  [Test]
  public void CheckStraight_NotPass()
  {
    var deal = new List<Card>
    {
      new(Face.Ace, Suit.Clubs),
      new(Face.Ace, Suit.Clubs),
      new(Face.Ace, Suit.Clubs),
      new(Face.King, Suit.Clubs),
      new(Face.Queen, Suit.Diamonds)
    };

    Assert.That(RankChecker.CheckStraight(deal), Is.False);
  }
  
  [Test]
  public void CheckStraightLowAce_Pass()
  {
    var deal = new List<Card>
    {
      new(Face.Four, Suit.Clubs),
      new(Face.Two, Suit.Clubs),
      new(Face.Three, Suit.Clubs),
      new(Face.Five, Suit.Clubs),
      new(Face.Ace, Suit.Clubs)
    };

    Assert.That(RankChecker.CheckStraight(deal), Is.True);
  }
  
  [Test]
  public void CheckStraightHighAce_Pass()
  {
    var deal = new List<Card>
    {
      new(Face.King, Suit.Clubs),
      new(Face.Ten, Suit.Clubs),
      new(Face.Queen, Suit.Clubs),
      new(Face.Jack, Suit.Clubs),
      new(Face.Ace, Suit.Clubs)
    };

    Assert.That(RankChecker.CheckStraight(deal), Is.True);
  }
  
  [Test]
  public void CheckStraightAce_NotPass()
  {
    var deal = new List<Card>
    {
      new(Face.Four, Suit.Clubs),
      new(Face.Two, Suit.Clubs),
      new(Face.Ten, Suit.Clubs),
      new(Face.Five, Suit.Clubs),
      new(Face.Ace, Suit.Clubs)
    };

    Assert.That(RankChecker.CheckStraight(deal), Is.False);
  }
  
  [Test]
  public void CheckRankUnknown_Pass()
  {
    var deal = new List<Card>
    {
      new(Face.Ace, Suit.Clubs),
      new(Face.Ace, Suit.Clubs),
      new(Face.Ace, Suit.Clubs),
      new(Face.Ace, Suit.Clubs)
    };

    Assert.That(RankChecker.Check(deal), Is.EqualTo(Rank.Unknown));
  }
  
  [Test]
  public void CheckRankRoyalFlush_Pass()
  {
    var deal = new List<Card>
    {
      new(Face.Ace, Suit.Clubs),
      new(Face.King, Suit.Clubs),
      new(Face.Jack, Suit.Clubs),
      new(Face.Ten, Suit.Clubs),
      new(Face.Queen, Suit.Clubs)
    };

    Assert.That(RankChecker.Check(deal), Is.EqualTo(Rank.RoyalFlush));
  }
  
  [Test]
  public void CheckRankStraightFlush_Pass()
  {
    var deal = new List<Card>
    {
      new(Face.Nine, Suit.Clubs),
      new(Face.King, Suit.Clubs),
      new(Face.Jack, Suit.Clubs),
      new(Face.Ten, Suit.Clubs),
      new(Face.Queen, Suit.Clubs)
    };

    Assert.That(RankChecker.Check(deal), Is.EqualTo(Rank.StraightFlush));
  }
  
  [Test]
  public void CheckRankQuads_Pass()
  {
    var deal = new List<Card>
    {
      new(Face.Queen, Suit.Hearts),
      new(Face.Queen, Suit.Diamonds),
      new(Face.Queen, Suit.Spades),
      new(Face.Ten, Suit.Clubs),
      new(Face.Queen, Suit.Clubs)
    };

    Assert.That(RankChecker.Check(deal), Is.EqualTo(Rank.Quads));
  }
  
  [Test]
  public void CheckRankFullHouse_Pass()
  {
    var deal = new List<Card>
    {
      new(Face.Queen, Suit.Hearts),
      new(Face.Queen, Suit.Diamonds),
      new(Face.Ten, Suit.Spades),
      new(Face.Ten, Suit.Clubs),
      new(Face.Queen, Suit.Clubs)
    };

    Assert.That(RankChecker.Check(deal), Is.EqualTo(Rank.FullHouse));
  }
  
  [Test]
  public void CheckRankFlush_Pass()
  {
    var deal = new List<Card>
    {
      new(Face.Nine, Suit.Clubs),
      new(Face.King, Suit.Clubs),
      new(Face.Two, Suit.Clubs),
      new(Face.Ten, Suit.Clubs),
      new(Face.Queen, Suit.Clubs)
    };

    Assert.That(RankChecker.Check(deal), Is.EqualTo(Rank.Flush));
  }
  
  [Test]
  public void CheckRankStraight_Pass()
  {
    var deal = new List<Card>
    {
      new(Face.Nine, Suit.Clubs),
      new(Face.King, Suit.Hearts),
      new(Face.Jack, Suit.Clubs),
      new(Face.Ten, Suit.Clubs),
      new(Face.Queen, Suit.Clubs)
    };

    Assert.That(RankChecker.Check(deal), Is.EqualTo(Rank.Straight));
  }
  
  [Test]
  public void CheckRankSet_Pass()
  {
    var deal = new List<Card>
    {
      new(Face.Queen, Suit.Hearts),
      new(Face.Queen, Suit.Diamonds),
      new(Face.Five, Suit.Spades),
      new(Face.Ten, Suit.Clubs),
      new(Face.Queen, Suit.Clubs)
    };

    Assert.That(RankChecker.Check(deal), Is.EqualTo(Rank.Set));
  }
  
  [Test]
  public void CheckRankTwoPairs_Pass()
  {
    var deal = new List<Card>
    {
      new(Face.Queen, Suit.Hearts),
      new(Face.King, Suit.Diamonds),
      new(Face.Ten, Suit.Spades),
      new(Face.Ten, Suit.Clubs),
      new(Face.Queen, Suit.Clubs)
    };

    Assert.That(RankChecker.Check(deal), Is.EqualTo(Rank.TwoPairs));
  }
  
  [Test]
  public void CheckRankOnePairs_Pass()
  {
    var deal = new List<Card>
    {
      new(Face.Queen, Suit.Hearts),
      new(Face.King, Suit.Diamonds),
      new(Face.Jack, Suit.Spades),
      new(Face.Ten, Suit.Clubs),
      new(Face.Queen, Suit.Clubs)
    };

    Assert.That(RankChecker.Check(deal), Is.EqualTo(Rank.OnePairs));
  }
  
  [Test]
  public void CheckRankHighCard_Pass()
  {
    var deal = new List<Card>
    {
      new(Face.Queen, Suit.Hearts),
      new(Face.King, Suit.Diamonds),
      new(Face.Jack, Suit.Spades),
      new(Face.Ten, Suit.Clubs),
      new(Face.Eight, Suit.Clubs)
    };

    Assert.That(RankChecker.Check(deal), Is.EqualTo(Rank.HighCard));
  }

}