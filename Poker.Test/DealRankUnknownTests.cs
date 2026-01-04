using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using NUnit.Framework;
using Poker.Enums;
using Poker.Implementation;

namespace Poker.Test;

public class DealRankUnknownTests
{
  [Test]
  public void CheckRankUnknown_WhenLessThanFiveCards()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    deal.AddCard(new Card(Face.Ace, Suit.Clubs));
    
    deal.Check();
    
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.Unknown));
      Assert.That(deal.RankDescription, Is.EqualTo(Rank.Unknown.GetDisplayName()));
      Assert.That(deal.RankName, Is.EqualTo(Rank.Unknown.ToString()));
      Assert.That(deal.RankCombo, Is.EqualTo(string.Empty));
    });
  }
}
