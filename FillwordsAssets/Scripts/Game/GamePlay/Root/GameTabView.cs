using UnityEngine;
using UnityEngine.UI;
using TMPro;
using R3;
namespace FillWords.Gameplay.Root
{
    public class GameTabView : GameTabViewBase
    {
        [SerializeField] TMP_Text LevelNumberTMP;
        [SerializeField] Button LeaveLevelBtn;
        [SerializeField] Button HideFieldBtn;
        [SerializeField] Button OpenSettingsBtn;
        public override void OnBind(GameTabViewModel viewModel)
        {
            base.OnBind(viewModel);
            viewModel.OnShowFieldEvent.Subscribe(_ => OnExtraFieldShow());
            viewModel.OnLevelTextEvent.Subscribe(levelText => LevelNumberTMP.text = levelText);
            LeaveLevelBtn.onClick.AddListener(() => viewModel.LeaveLevelEvent.OnNext(Unit.Default));
            HideFieldBtn.onClick.AddListener(() => OnHideFieldBtn());
            OpenSettingsBtn.onClick.AddListener(() => viewModel.OpenSettingsEvent.OnNext(Unit.Default));

            SetDefaultState();
        }
        void OnExtraFieldShow()
        {
            LeaveLevelBtn.gameObject.SetActive(false);
            OpenSettingsBtn.gameObject.SetActive(false);
            HideFieldBtn.gameObject.SetActive(true);
        }
        void OnHideFieldBtn()
        {
            SetDefaultState();
            viewModel.HideFieldEvent.OnNext(Unit.Default);
        }
        void SetDefaultState()
        {
            LeaveLevelBtn.gameObject.SetActive(true);
            OpenSettingsBtn.gameObject.SetActive(true);
            HideFieldBtn.gameObject.SetActive(false);
        }
    }
}
