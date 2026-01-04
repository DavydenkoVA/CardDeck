using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using NUnit.Framework;
using Poker.Enums;
using Poker.Implementation;

namespace Poker.Test;

public class DealRankStraightFlushTests
{
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
  public void CheckRankStraightFlush_WithJoker()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Eight, Suit.Hearts));
    deal.AddCard(new Card(Face.Ten, Suit.Hearts));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    deal.AddCard(new Card(Face.Queen, Suit.Hearts));
    deal.AddCard(new Card(Face.Jack, Suit.Hearts));
    
    deal.Check();
    
    Assert.That(deal.Rank, Is.EqualTo(Rank.StraightFlush));
    Assert.That(deal.RankCombo, Is.EqualTo($"{Face.Queen.GetDisplayName()}{Suit.Hearts.GetDisplayName()}"));
  }
}
