using System;
using R3;
namespace FillWords.Gameplay 
{ 
    public class VictoryTabViemModel 
    {
        readonly VictoryTabModel model;
        public readonly Subject<Action> OpeningEvent;
        public readonly Subject<Action> ClosingEvent;
        public readonly Subject<Unit> NextLevelReqEvent;
        public readonly Subject<Unit> ShowFieldReqEvent;
        const string levelTextPattern = "Level {0}";
        public VictoryTabViemModel(VictoryTabModel _model)
        {
            model = _model;
            OpeningEvent = model.OpeningEvent;
            ClosingEvent = model.ClosingEvent;
            NextLevelReqEvent = new();
            ShowFieldReqEvent = new();
            NextLevelReqEvent.ThrottleFirst(TimeSpan.FromSeconds(1)).Subscribe(_ => OnNextLevelBtn());
            ShowFieldReqEvent.ThrottleFirst(TimeSpan.FromSeconds(0.5f)).Subscribe(_ => OnShowFieldBtn());
        }
        void OnNextLevelBtn()
        {
            model.NextLevel();
        }
        void OnShowFieldBtn()
        {
            model.ShowField();
        }
        public string GetNextLevelText()
        {
            int nextLevelNum = model.CurrentLevel + 1;
            return string.Format(levelTextPattern, nextLevelNum);
        }
    }
}
