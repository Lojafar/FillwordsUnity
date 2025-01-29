using System;
using R3;

namespace FillWords.MainMenu
{
    public class HomeTabViewModel 
    {
        readonly HomeTabModel model;
        public readonly Subject<Unit> PlayButtonEvent;
        public readonly Subject<Unit> SettingsButtonEvent;
        public readonly Subject<Unit> HomeButtonEvent;
        public readonly Subject<Action> OpeningEvent;
        public readonly Subject<Action> ClosingEvent;
        public readonly ReactiveProperty<string> LevelText;
        const string levelTextPattern = "Level {0}";
        public HomeTabViewModel(HomeTabModel _model)
        {
            model = _model;
            LevelText = new();
            PlayButtonEvent = new();
            model.level.Subscribe(levelNum => LevelText.OnNext(string.Format(levelTextPattern, levelNum)));
            PlayButtonEvent.ThrottleFirst(TimeSpan.FromSeconds(0.3)).Subscribe(_ => model.OnPlayGame());
            OpeningEvent = model.OpeningEvent;
            ClosingEvent = model.ClosingEvent;
        }

    }
}
