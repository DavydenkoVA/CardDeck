using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using NUnit.Framework;
using Poker.Enums;
using Poker.Implementation;

namespace Poker.Test;

public class DealRankOnePairTests
{
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
      Assert.That(deal.RankName, Is.EqualTo(nameof(Rank.OnePairs)));
      Assert.That(deal.RankCombo, Is.EqualTo(Face.Queen.GetDisplayName()));
    });
  }
  
  [Test]
  public void CheckRankOnePairs_WithJoker()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Ace, Suit.Hearts));
    deal.AddCard(new Card(Face.King, Suit.Diamonds));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Three, Suit.Clubs));
    
    deal.Check();
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.OnePairs));
      Assert.That(deal.RankCombo, Is.EqualTo(Face.Ace.GetDisplayName()));
    });
  }
  
  [Test]
  public void CheckRankOnePairs_WithTwoJoker()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Ace, Suit.Hearts));
    deal.AddCard(new Card(Face.King, Suit.Diamonds));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    
    deal.Check();
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, !Is.EqualTo(Rank.OnePairs));
    });
  }
}
