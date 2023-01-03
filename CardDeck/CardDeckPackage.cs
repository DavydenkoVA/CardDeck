using Poker.Implementation;
using Poker.Interfaces;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace CardDeck;

public class CardDeckPackage : IPackage
{
  public void RegisterServices(Container container)
  {
    container.RegisterSingleton<IDeck, PokerDeck>();
  }
}