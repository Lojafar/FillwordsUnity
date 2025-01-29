using System;
using UnityEngine;
using DG.Tweening;
namespace FillWords.Root.UI.Tabs
{
    public abstract class ScalingTabViewBase<TViewModel> : TabViewBase<TViewModel> where TViewModel : class
    {
        Tween activeTween;
        const float OpenAnimTime = 0.6f;
        const float CloseAnimTime = 0.25f;
        const float MinTabScale = 0.15f;
        public override void Open(Action onCompleted)
        {
            gameObject.SetActive(true);
            activeTween?.Kill();
            activeTween = transform.DOScale(Vector3.one, OpenAnimTime).OnComplete(()=>
            {
                activeTween = null;
                onCompleted?.Invoke();
            });
        }
        public override void Close(Action onCompleted)
        {
            activeTween?.Kill();
            activeTween = transform.DOScale(Vector3.one * MinTabScale, CloseAnimTime).OnComplete(() =>
            {
                activeTween = null;
                gameObject.SetActive(false);
                onCompleted?.Invoke();
            });
        }
    }
}
