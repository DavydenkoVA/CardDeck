using CardDeck.Core.Base;
using CardDeck.Core.Enums;
using NUnit.Framework;
using Poker.Enums;
using Poker.Implementation;

namespace Poker.Test;

public class DealRankTwoPairsTests
{
  [Test]
  public void CheckRankTwoPairs_Pass()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Queen, Suit.Hearts));
    deal.AddCard(new Card(Face.King, Suit.Diamonds));
    deal.AddCard(new Card(Face.Ten, Suit.Spades));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Queen, Suit.Clubs));
    
    deal.Check();
    
    Assert.Multiple(() =>
    {
      Assert.That(deal.Rank, Is.EqualTo(Rank.TwoPairs));
      Assert.That(deal.RankDescription, Is.EqualTo(Rank.TwoPairs.GetDisplayName()));
      Assert.That(deal.RankName, Is.EqualTo(Rank.TwoPairs.ToString()));
      Assert.That(deal.RankCombo, Is.EqualTo($"{Face.Queen.GetDisplayName()},{Face.Ten.GetDisplayName()}"));
    });
  }
  
  [Test]
  public void CheckRankTwoPairs_WithJoker()
  {
    var deal = new Deal();
    
    deal.AddCard(new Card(Face.Queen, Suit.Hearts));
    deal.AddCard(new Card(Face.King, Suit.Diamonds));
    deal.AddCard(new Card(Face.Jocker, Suit.Jocker));
    deal.AddCard(new Card(Face.Ten, Suit.Clubs));
    deal.AddCard(new Card(Face.Queen, Suit.Clubs));
    
    deal.Check();
    
    Assert.That(deal.Rank, Is.EqualTo(Rank.TwoPairs));
    Assert.That(deal.RankCombo, Is.EqualTo($"{Face.Queen.GetDisplayName()},{Face.Ten.GetDisplayName()}"));
  }
}
