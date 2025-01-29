using FillWords.Root.UI.Tabs;
using FillWords.Root.SaveLoad;
using FillWords.Root.Data;
using System;
using R3;

namespace FillWords.Gameplay
{
    public class MiniSettingsTabModel : ITabModel
    {
        public readonly Subject<Action> OpeningEvent;
        public readonly Subject<Action> ClosingEvent;
        public readonly ReadOnlyReactiveProperty<float> MusicVolume;
        public readonly ReadOnlyReactiveProperty<float> SoundsVolume;
        readonly ParentTabModel parentTabModel;
        readonly AllDataContainer dataContainer;
        readonly ISaverLoader saverLoader;
        bool settingsChanged;
        public MiniSettingsTabModel(ParentTabModel _parentTabModel, AllDataContainer _dataContainer, ISaverLoader _saverLoader)
        {
            parentTabModel = _parentTabModel;
            dataContainer = _dataContainer;
            saverLoader = _saverLoader;
            OpeningEvent = new();
            ClosingEvent = new();
            MusicVolume = dataContainer.SettingsData.MusicVolume;
            SoundsVolume = dataContainer.SettingsData.SoundsVolume;
            settingsChanged = false;
        }
        public void SetMusicVolume(float volume)
        {
            dataContainer.SettingsData.MusicVolume.Value = volume;
            settingsChanged = true;
        }
        public void SetSFXVolume(float volume)
        {
            dataContainer.SettingsData.SoundsVolume.Value = volume;
            settingsChanged = true;
        }
        public void OnBackInput()
        {
            parentTabModel.ClosePopup<MiniSettingsTabModel>();
        }
        public void Open(Action onCompleted)
        {
            OpeningEvent.OnNext(onCompleted);
        }
        public void Close(Action onCompleted)
        {
            if (settingsChanged)
            {
                saverLoader.SaveSettings(dataContainer.SettingsData.DataOrigin);
                settingsChanged = false;
            }
            ClosingEvent.OnNext(onCompleted);
        }
    }
}
