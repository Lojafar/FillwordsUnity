using UnityEngine;
using UnityEngine.UI;
using TMPro;
using R3;

namespace FillWords.MainMenu
{
    class HomeTabView : HomeTabViewBase
    {
        [SerializeField] Button playButton;
        [SerializeField] TMP_Text levelNumberText;
        public override void OnBind(HomeTabViewModel viewModel)
        {
            base.OnBind(viewModel);
            playButton.onClick.AddListener(() => viewModel.PlayButtonEvent.OnNext(Unit.Default));
            viewModel.LevelText.Subscribe(newLevel => levelNumberText.text = newLevel);
        }
    }
}
