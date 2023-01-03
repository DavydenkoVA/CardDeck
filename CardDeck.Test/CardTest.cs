using CardDeck.Enums;
using CardDeck.Implementation;
using NUnit.Framework;

namespace CardDeck.Test;

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