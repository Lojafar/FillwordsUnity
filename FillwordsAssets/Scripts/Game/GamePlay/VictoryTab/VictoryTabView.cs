using System;
using R3;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace FillWords.Gameplay
{
    public class VictoryTabView : VictoryTabViewBase
    {
        [SerializeField] Button nextLevelButton;
        [SerializeField] Button showFieldButton;
        [SerializeField] TMP_Text nextLevelText;
        public override void OnBind(VictoryTabViemModel viewModel)
        {
            base.OnBind(viewModel);
            nextLevelButton.onClick.AddListener(() => viewModel.NextLevelReqEvent.OnNext(Unit.Default));
            showFieldButton.onClick.AddListener(() => viewModel.ShowFieldReqEvent.OnNext(Unit.Default));
        }
        public override void Open(Action onCompleted)
        {
            nextLevelText.text = viewModel.GetNextLevelText();
            base.Open(onCompleted);
        }
    }
}
