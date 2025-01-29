using FillWords.Root.UI.Tabs;
using FillWords.Root.Data;
using FillWords.Root.SaveLoad;
using FillWords.Gameplay.Root;
using System;
using R3;

namespace FillWords.Gameplay
{
    public class VictoryTabModel : ITabModel
    {
        public readonly Subject<Action> OpeningEvent;
        public readonly Subject<Action> ClosingEvent;
        readonly GameTabModel gameTabModel;
        readonly AllDataContainer dataContainer;
        readonly ISaverLoader saverLoader;
        public int CurrentLevel => dataContainer.ProgressData.CurrentLevel.Value;
        public VictoryTabModel(GameTabModel _gameTabModel, AllDataContainer _dataContainer, ISaverLoader _saverLoader)
        {
            OpeningEvent = new();
            ClosingEvent = new();
            gameTabModel = _gameTabModel;
            dataContainer = _dataContainer;
            saverLoader = _saverLoader;
        }
        public void NextLevel()
        {
            dataContainer.ProgressData.CurrentLevel.Value++;
            saverLoader.SaveProgress(dataContainer.ProgressData.DataOrigin);
            gameTabModel.ReplayLevel();
        }
        public void ShowField()
        {
            gameTabModel.ShowVictoryField();
        }
        public void Open(Action onCompleted)
        {
            OpeningEvent.OnNext(onCompleted);
        }
        public void Close(Action onCompleted)
        {
            ClosingEvent.OnNext(onCompleted);
        }
    }
}
