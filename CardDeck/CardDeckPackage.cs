using System.Diagnostics.CodeAnalysis;
using Poker.Implementation;
using Poker.Interfaces;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace CardDeck;

[ExcludeFromCodeCoverage]
public class CardDeckPackage : IPackage
{
  public void RegisterServices(Container container)
  {
    container.RegisterSingleton<IPokerDeck, PokerDeck>();
  }
}