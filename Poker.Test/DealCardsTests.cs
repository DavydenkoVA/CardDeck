using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using NUnit.Framework;

namespace Poker.Test;

public class DealCardsTests
{
  [Test]
  public void Cards_ReturnsFormattedString()
  {
    var deal = new Poker.Implementation.Deal();

    var card1 = new Card(Face.Ace, Suit.Clubs);
    var card2 = new Card(Face.Ten, Suit.Diamonds);
    
    deal.AddCard(card1);
    deal.AddCard(card2);

    Assert.That(deal.Cards, Is.EqualTo($"│{card1.CardName}│{card2.CardName}│"));
  }
}
