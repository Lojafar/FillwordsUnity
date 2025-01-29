using FillWords.Root.UI;
using FillWords.Root.UI.Tabs;
using FillWords.Root.Data;
using FillWords.Gameplay.Root;
using System;
using R3;
namespace FillWords.MainMenu
{
    public class HomeTabModel : ITabModel
    {
        public readonly Subject<Action> OpeningEvent;
        public readonly Subject<Action> ClosingEvent;
        public readonly ReadOnlyReactiveProperty<int> level;
        readonly AllDataContainer dataContainer;
        readonly TabsHandler tabsHandler;
        public HomeTabModel(AllDataContainer _dataContainer, TabsHandler _tabsHandler)
        {
            dataContainer = _dataContainer;
            tabsHandler = _tabsHandler;
            level = dataContainer.ProgressData.CurrentLevel;
            OpeningEvent = new();
            ClosingEvent = new();
        }
        public void OnPlayGame()
        {
            tabsHandler.OpenTab<GameTabModel>();
        }
        public void Open(Action onComleted)
        {
            OpeningEvent.OnNext(onComleted);
        }
        public void Close(Action onComleted)
        {
            ClosingEvent.OnNext(onComleted);
        }
    }
}
