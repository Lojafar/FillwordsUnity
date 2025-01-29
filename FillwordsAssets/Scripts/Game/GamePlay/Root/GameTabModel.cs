using FillWords.Root.UI;
using FillWords.Root.UI.Tabs;
using FillWords.Root.Data;
using FillWords.MainMenu.Root;
using FillWords.Gameplay.Game.Root;
using System;
using R3;
namespace FillWords.Gameplay.Root
{
    public class GameTabModel : ParentTabModel
    {
        readonly TabsHandler tabsHandler;
        readonly AllDataContainer allDataContainer;
        public readonly Subject<Unit> OnPlayLevel;
        public readonly Subject<Unit> OnShowFieldEvent;
        public int LevelNumber => allDataContainer.ProgressData.CurrentLevel.CurrentValue;

        public GameTabModel(TabsHandler _tabsHandler, AllDataContainer _allDataContainer)
        {
            tabsHandler = _tabsHandler;
            allDataContainer = _allDataContainer;
            OnPlayLevel = new();
            OnShowFieldEvent = new();
        }
        public void CompleteLevel()
        {
            OpenTab<VictoryTabModel>();
        }
        public void OpenSettingsPopup()
        {
            OpenPopup<MiniSettingsTabModel>();
        }
        public void ReplayLevel()
        {
            Open(null);
        }
        public void LeaveLevel()
        {
            tabsHandler.OpenTab<MainMenuModel>();
        }
        public void ShowVictoryField()
        {
            OpenTab<GameFieldModel>();
            OnShowFieldEvent.OnNext(Unit.Default);
        }
        public void HideVictoryField()
        {
            OpenTab<VictoryTabModel>();
        }
        public override void Open(Action onCompleted)
        {
            base.Open(onCompleted);
            OpenTab<GameFieldModel>();

            OnPlayLevel.OnNext(Unit.Default);
        }
    }
}
