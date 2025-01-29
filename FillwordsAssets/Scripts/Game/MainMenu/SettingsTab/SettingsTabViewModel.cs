using System;
using R3;
namespace FillWords.MainMenu
{
    public class SettingsTabViewModel
    {
        public readonly Subject<Action> OpeningEvent;
        public readonly Subject<Action> ClosingEvent;
        public readonly ReactiveProperty<float> MusicVolume;
        public readonly ReactiveProperty<float> SFXVolume;

        const float maxVolume = 1;
        const float minVolume = 0;
        public SettingsTabViewModel(SettingsTabModel model)
        {
            OpeningEvent = model.OpeningEvent;
            ClosingEvent = model.ClosingEvent;
            MusicVolume = new();
            SFXVolume = new();
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
        void CheckAndCorrectVolume(ref float volume)
        {
            if (volume < minVolume) volume = minVolume;
            else if (volume > maxVolume) volume = maxVolume;
        }
    }
}
