using UnityEngine;
using R3;
namespace FillWords.MainMenu
{
    public class SettingsTabView : SettingsTabViewBase
    {
        [SerializeField] ExtendedSlider MusicSlider;
        [SerializeField] ExtendedSlider SoundsSlider;
        public override void OnBind(SettingsTabViewModel viewModel)
        {
            base.OnBind(viewModel);

            viewModel.MusicVolume.Subscribe(volume => MusicSlider.value = volume);
            viewModel.SFXVolume.Subscribe(volume => SoundsSlider.value = volume);

            MusicSlider.OnEndChanging += Volume => viewModel.MusicVolume.Value = Volume;
            SoundsSlider.OnEndChanging += Volume => viewModel.SFXVolume.Value = Volume;
        }
    }
}
