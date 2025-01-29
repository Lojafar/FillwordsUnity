using System;
using R3;
namespace FillWords.Gameplay.Root
{
    public class GameTabViewModel
    {
        public readonly Subject<Unit> LeaveLevelEvent;
        public readonly Subject<Unit> HideFieldEvent;
        public readonly Subject<Unit> OpenSettingsEvent;
        public readonly Subject<Unit> OnShowFieldEvent;
        public readonly Subject<Action> OpeningEvent;
        public readonly Subject<Action> ClosingEvent;
        public readonly Subject<string> OnLevelTextEvent;
        const string LevelTextPattern = "Level {0}";
        public GameTabViewModel(GameTabModel model)
        {
            LeaveLevelEvent =new();
            HideFieldEvent = new();
            OpenSettingsEvent = new();
            OnLevelTextEvent = new();
            OpeningEvent = new();
            OnShowFieldEvent = model.OnShowFieldEvent;
            ClosingEvent = model.ClosingEvent;
            model.OpeningEvent.Subscribe(onCompleted => 
            {
                OnLevelTextEvent.OnNext(string.Format(LevelTextPattern, model.LevelNumber));
                OpeningEvent.OnNext(onCompleted);
            });

            LeaveLevelEvent.ThrottleFirst(TimeSpan.FromSeconds(0.3)).Subscribe(_ => model.LeaveLevel());
            HideFieldEvent.ThrottleFirst(TimeSpan.FromSeconds(0.3)).Subscribe(_ => model.HideVictoryField());
            OpenSettingsEvent.ThrottleFirst(TimeSpan.FromSeconds(0.3)).Subscribe(_ => model.OpenSettingsPopup());
        }
    }
}
