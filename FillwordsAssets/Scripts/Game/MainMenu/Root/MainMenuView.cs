using UnityEngine;
using UnityEngine.UI;
using R3;

namespace FillWords.MainMenu.Root
{
    class MainMenuView : MainMenuViewBase
    {
        [SerializeField] Button settingsButton;
        [SerializeField] Button homeButton;
        public override void OnBind(MainMenuViewModel viewModel)
        {
            base.OnBind(viewModel);
            settingsButton.onClick.AddListener(() => viewModel.SettingsButtonEvent.OnNext(Unit.Default));
            homeButton.onClick.AddListener(() => viewModel.HomeButtonEvent.OnNext(Unit.Default));
        }
    }
}