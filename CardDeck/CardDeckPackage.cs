using CardDeck.Implementation;
using CardDeck.Interfaces;
using SimpleInjector;
using SimpleInjector.Packaging;
using ViennaNET.SimpleInjector.Extensions;

namespace CardDeck;

public class CardDeckPackage : IPackage
{
  public void RegisterServices(Container container)
  {
    container.RegisterSingleton<IDeck, Deck>();
  }
}