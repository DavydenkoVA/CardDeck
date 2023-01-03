using System;
using System.Collections.Generic;
using System.Linq;
using CardDeck.Dtos;
using CardDeck.Enums;
using CardDeck.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CardDeck.Implementation;

public class Deck : IDeck
{
  private const short NumberOfCards = 52;
  private readonly Card[] _deck;
  private int _currentCard;
  private readonly Random _rnd;

  public Deck()
  {

    _deck = new Card[NumberOfCards];
    _currentCard = 0;
    _rnd = new Random();
    for (var count = 0; count < _deck.Length; count++)
    {
      _deck[count] = new Card((Face)(count % 13), (Suit)(count / 13));
    }
    Shuffle();
  }

  private void Shuffle()
  {
    _currentCard = 0;
    for (var first = 0; first < _deck.Length; first++)
    {
      var second = _rnd.Next(NumberOfCards);
      (_deck[first], _deck[second]) = (_deck[second], _deck[first]);
    }
  }

  public ActionResult DealCard()
  {
    while (true)
    {
      if (_currentCard + 5 < _deck.Length)
      {
        var deal = new List<Card>
        {
          _deck[_currentCard++],
          _deck[_currentCard++],
          _deck[_currentCard++],
          _deck[_currentCard++],
          _deck[_currentCard++]
        };
        var rank = RankChecker.Check(deal);

        var result = new DealDtp
        {
          Cards = "|" + string.Join('|', deal) + "|",
          Rank = rank, 
          RankName = rank.ToString(),
          RankDescription = rank.GetDisplayName(),
        };

        return result;
      }
      
      Shuffle();
    }
  }
}