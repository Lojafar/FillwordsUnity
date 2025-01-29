using FillWords.Root.UI.Tabs;
using System;
using R3;
namespace FillWords.Gameplay
{
    public class MiniSettingsTabVM 
    {
        readonly MiniSettingsTabModel model;
        public readonly Subject<Unit> BackBtnEvent;
        public readonly Subject<Action> OpeningEvent;
        public readonly Subject<Action> ClosingEvent;
        public readonly ReactiveProperty<float> MusicVolume;
        public readonly ReactiveProperty<float> SFXVolume;

        const float maxVolume = 1;
        const float minVolume = 0;
        public MiniSettingsTabVM(MiniSettingsTabModel _model)
        {
            model = _model;
            BackBtnEvent = new();
            MusicVolume = new();
            SFXVolume = new();
            BackBtnEvent.ThrottleFirst(TimeSpan.FromSeconds(0.5f)).Subscribe(_ => BackBtnInput());
            OpeningEvent = model.OpeningEvent;
            ClosingEvent = model.ClosingEvent;
            model.MusicVolume.Subscribe(volume => MusicVolume.Value = volume);
            model.SoundsVolume.Subscribe(volume => SFXVolume.Value = volume);
            MusicVolume.Skip(1).Subscribe(newVolume =>
            {
                CheckAndCorrectVolume(ref newVolume);
                model.SetMusicVolume(newVolume);
            });
            SFXVolume.Skip(1).Subscribe(newVolume =>
            {
                CheckAndCorrectVolume(ref newVolume);
                model.SetSFXVolume(newVolume);
            });
        }
        void BackBtnInput()
        {
            model.OnBackInput();
        }
        void CheckAndCorrectVolume(ref float volume)
        {
            if (volume < minVolume) volume = minVolume;
            else if (volume > maxVolume) volume = maxVolume;
        }
    }
}
