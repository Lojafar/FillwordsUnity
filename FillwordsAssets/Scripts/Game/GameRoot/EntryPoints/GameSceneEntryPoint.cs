using FillWords.Root.UI;
using FillWords.MainMenu.Root;
using Zenject;
namespace FillWords.Root.EntryPoint
{
    class GameSceneEntryPoint : IInitializable
    {
        readonly TabsHandler tabsHandler;
        public GameSceneEntryPoint(TabsHandler _tabsHandler)
        {
            tabsHandler = _tabsHandler;
        }
        public void Initialize()
        {
            tabsHandler.OpenTab<MainMenuModel>();
        }
    }
}
