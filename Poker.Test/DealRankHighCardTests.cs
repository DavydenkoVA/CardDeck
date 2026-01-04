using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using NUnit.Framework;
using Poker.Enums;
using Poker.Implementation;

namespace Poker.Test;

public class DealRankHighCardTests
{
  [Test]
  public void CheckRankHighCard_Pass()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Queen, Suit.Hearts));
    deal.AddCard(new Card(Face.King, Suit.Diamonds));
    deal.AddCard(new Card(Face.Jack, Suit.Spades));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Eight, Suit.Clubs));
    
    deal.Check();
    
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.HighCard));
      Assert.That(deal.RankDescription, Is.EqualTo(Rank.HighCard.GetDisplayName()));
      Assert.That(deal.RankName, Is.EqualTo(Rank.HighCard.ToString()));
      Assert.That(deal.RankCombo, Is.EqualTo(Face.King.GetDisplayName()));
    });
  }
}
