using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using NUnit.Framework;
using Poker.Implementation;

namespace Poker.Test;

public class StraightDetectionTests
{
  [Test]
  public void CheckStraight_ReturnsTrue_ForSequentialCards()
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
  public void CheckStraight_ReturnsFalse_ForNonSequentialCards()
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
  public void CheckStraight_ReturnsTrue_ForLowAceStraight()
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
  public void CheckStraight_ReturnsTrue_ForHighAceStraight()
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
  public void CheckStraight_ReturnsFalse_ForAceGap()
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
  public void CheckStraight_ReturnsTrue_WithJokerCompletingSequence()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    deal.AddCard(new Card(Face.King, Suit.Clubs));
    deal.AddCard(new Card(Face.Queen, Suit.Clubs));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    deal.AddCard(new Card(Face.Jack, Suit.Clubs));
    
    Assert.That(deal.CheckStraight(), Is.True);
  }
}
