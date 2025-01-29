using System;
using R3;
namespace FillWords.MainMenu.Root
{
    public class MainMenuViewModel
    {
        readonly MainMenuModel model;
        public readonly Subject<Unit> SettingsButtonEvent;
        public readonly Subject<Unit> HomeButtonEvent;
        public readonly Subject<Action> OpeningEvent;
        public readonly Subject<Action> ClosingEvent;
        public MainMenuViewModel(MainMenuModel _model)
        {
            model = _model;
            SettingsButtonEvent = new();
            HomeButtonEvent = new();
            SettingsButtonEvent.ThrottleFirst(TimeSpan.FromSeconds(0.3f)).Subscribe(_ => model.OnSettingsBtn());
            HomeButtonEvent.ThrottleFirst(TimeSpan.FromSeconds(0.3f)).Subscribe(_ => model.OnHomeBtn());
            OpeningEvent = model.OpeningEvent;
            ClosingEvent = model.ClosingEvent;
        }
        
    }
}
