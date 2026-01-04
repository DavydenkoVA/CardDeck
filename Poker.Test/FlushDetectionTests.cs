using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using NUnit.Framework;
using Poker.Implementation;

namespace Poker.Test;

public class FlushDetectionTests
{
  [Test]
  public void CheckFlush_ReturnsTrue_ForFiveSameSuit()
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
  public void CheckFlush_ReturnsFalse_ForMixedSuits()
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
  public void CheckFlush_ReturnsTrue_WithJokersCompletingSuit()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Ace, Suit.Hearts));
    deal.AddCard(new Card(Face.King, Suit.Hearts));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    deal.AddCard(new Card(Face.Queen, Suit.Hearts));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));

    Assert.That(deal.CheckFlush(), Is.True);
  }
}
