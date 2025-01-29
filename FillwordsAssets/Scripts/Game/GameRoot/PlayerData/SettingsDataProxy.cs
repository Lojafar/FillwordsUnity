using R3;

namespace FillWords.Root.Data
{
    public class SettingsDataProxy
    {
        public readonly ReactiveProperty<float> MusicVolume;
        public readonly ReactiveProperty<float> SoundsVolume;
        public readonly SettingsData DataOrigin;

        const float maxVolume = 1;
        const float minVolume = 0;
        public SettingsDataProxy(SettingsData _settingsData)
        {
            DataOrigin = _settingsData;
            MusicVolume = new ReactiveProperty<float>(DataOrigin.MusicVolume);
            SoundsVolume = new ReactiveProperty<float>(DataOrigin.SoundsVolume);
            MusicVolume.Skip(1).Subscribe(newVolume =>
            {
                if (CheckVolume(newVolume)) DataOrigin.MusicVolume = newVolume;
            });
            SoundsVolume.Skip(1).Subscribe(newVolume =>
            {
                if (CheckVolume(newVolume)) DataOrigin.SoundsVolume = newVolume;
            });
        }
        bool CheckVolume(float volume)
        {
            return volume >= minVolume && volume <= maxVolume;
        }
    }
}
