using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using NUnit.Framework;
using Poker.Enums;
using Poker.Implementation;

namespace Poker.Test;

public class DealRankRoyalFlushTests
{
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
  public void CheckRankRoyalFlush_WithJoker()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Ace, Suit.Spades));
    deal.AddCard(new Card(Face.King, Suit.Spades));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    deal.AddCard(new Card(Face.Ten, Suit.Spades));
    deal.AddCard(new Card(Face.Queen, Suit.Spades));
    
    deal.Check();
    
    Assert.That(deal.Rank, Is.EqualTo(Rank.RoyalFlush));
    Assert.That(deal.RankCombo, Is.EqualTo($"{Face.Ace.GetDisplayName()}{Suit.Spades.GetDisplayName()}"));
  }
}
