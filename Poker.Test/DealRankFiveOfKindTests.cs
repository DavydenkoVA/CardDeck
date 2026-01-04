using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using NUnit.Framework;
using Poker.Enums;
using Poker.Implementation;

namespace Poker.Test;

public class DealRankFiveOfKindTests
{
  [Test]
  public void CheckRankFiveOfKind_WithTwoJokers()
    {
        var deal = new Deal();
    
    deal.AddCard(new Card(Face.King, Suit.Hearts));
    deal.AddCard(new Card(Face.King, Suit.Diamonds));
    deal.AddCard(new Card(Face.King, Suit.Spades));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    
    deal.Check();
        Assert.Multiple(() =>
        {
            Assert.That(deal.Rank, Is.EqualTo(Rank.FiveOfKind));
            Assert.That(deal.RankCombo, Is.EqualTo(Face.King.GetDisplayName()));
        });
    }

    [Test]
  public void CheckRankFiveOfKind_WithOneJoker()
  { 
    var deal = new Deal();
  
    deal.AddCard(new Card(Face.Queen, Suit.Hearts));
    deal.AddCard(new Card(Face.Queen, Suit.Diamonds));
    deal.AddCard(new Card(Face.Queen, Suit.Spades));
    deal.AddCard(new Card(Face.Queen, Suit.Clubs));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    
    deal.Check();
    
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.FiveOfKind));
      Assert.That(deal.RankCombo, Is.EqualTo(Face.Queen.GetDisplayName()));
    });
  }
}
