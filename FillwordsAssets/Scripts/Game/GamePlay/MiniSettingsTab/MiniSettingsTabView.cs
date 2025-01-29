using R3;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace FillWords.Gameplay
{
    class MiniSettingsTabView : MiniSettingsViewBase
    {
        [SerializeField] Vector3 normalTabPosition;
        [SerializeField] Vector3 hidenTabPosition;
        [SerializeField] ExtendedSlider MusicSlider;
        [SerializeField] ExtendedSlider SFXSlider;
        [SerializeField] Button backButton;
        public override void OnBind(MiniSettingsTabVM viewModel)
        {
            base.OnBind(viewModel);
            viewModel.MusicVolume.Subscribe(volume => MusicSlider.value = volume);
            viewModel.SFXVolume.Subscribe(volume => SFXSlider.value = volume);
            MusicSlider.OnEndChanging += Volume => viewModel.MusicVolume.Value = Volume;
            SFXSlider.OnEndChanging += Volume => viewModel.SFXVolume.Value = Volume;
            backButton.onClick.AddListener(() => viewModel.BackBtnEvent.OnNext(Unit.Default));
        }
        public override void Open(Action onCompleted)
        {
            gameObject.SetActive(true);
            transform.DOLocalMove(normalTabPosition, 1).From(hidenTabPosition).OnComplete(() => onCompleted?.Invoke());
        }
        public override void Close(Action onCompleted)
        {
            transform.DOLocalMove(hidenTabPosition, 1).From(normalTabPosition)
               .OnComplete(() =>
               {
                   gameObject.SetActive(false);
                   onCompleted?.Invoke();
               });
        }
    }
}
