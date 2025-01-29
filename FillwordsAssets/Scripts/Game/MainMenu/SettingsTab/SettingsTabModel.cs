using FillWords.Root.UI.Tabs;
using FillWords.Root.SaveLoad;
using FillWords.Root.Data;
using System;
using R3;

namespace FillWords.MainMenu 
{
    public class SettingsTabModel : ITabModel
    {
        public readonly Subject<Action> OpeningEvent;
        public readonly Subject<Action> ClosingEvent;
        public readonly ReadOnlyReactiveProperty<float> MusicVolume;
        public readonly ReadOnlyReactiveProperty<float> SoundsVolume;
        readonly AllDataContainer dataContainer;
        readonly ISaverLoader saverLoader;
        bool settingsChanged;

        public SettingsTabModel(AllDataContainer _dataContainer, ISaverLoader _saverLoader)
        {
            dataContainer = _dataContainer;
            saverLoader = _saverLoader;
            OpeningEvent = new();
            ClosingEvent = new();
            MusicVolume = dataContainer.SettingsData.MusicVolume;
            SoundsVolume = dataContainer.SettingsData.SoundsVolume;
            settingsChanged = false;
        }
        public void SetMusicVolume(float newVolume)
        {
            dataContainer.SettingsData.MusicVolume.Value = newVolume;
            settingsChanged = true;
        }
        public void SetSFXVolume(float newVolume)
        {
            dataContainer.SettingsData.SoundsVolume.Value = newVolume;
            settingsChanged = true;
        }
        public void Open(Action onCompleted)
        {
            OpeningEvent.OnNext(onCompleted);
        }
        public void Close(Action onCompleted)
        {
            if(settingsChanged)
            {
                saverLoader.SaveSettings(dataContainer.SettingsData.DataOrigin);
                settingsChanged = false;
            }
            ClosingEvent.OnNext(onCompleted);
        }
    }
}
