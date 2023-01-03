using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using NUnit.Framework;

namespace Poker.Test;

public class CardTest
{
  [SetUp]
  public void Setup()
  {
  }

  [Test]
  public void CardToString_Pass()
  {
    var card = new Card(Face.King, Suit.Diamonds);
    
    Assert.That(card.ToString(), Is.EqualTo("К♦"));
  }
}