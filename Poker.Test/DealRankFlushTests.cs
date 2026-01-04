using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using NUnit.Framework;
using Poker.Enums;
using Poker.Implementation;

namespace Poker.Test;

public class DealRankFlushTests
{
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
  public void CheckRankFlush_WithJoker()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Nine, Suit.Hearts));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    deal.AddCard(new Card(Face.Two, Suit.Hearts));
    deal.AddCard(new Card(Face.Ten, Suit.Hearts));
    deal.AddCard(new Card(Face.Queen, Suit.Hearts));
    
    deal.Check();
    
    Assert.That(deal.Rank, Is.EqualTo(Rank.Flush));
    Assert.That(deal.RankCombo, Is.EqualTo(Suit.Hearts.GetDisplayName()));
  }
}
