using UnityEngine;
using System;
namespace FillWords.Root.UI.Tabs
{
    public abstract class TabViewBase<TViewModel> : MonoBehaviour where TViewModel : class
    {
        protected TViewModel viewModel;
        public void Bind(TViewModel _viewModel)
        {
            viewModel = _viewModel;
            OnBind(_viewModel);
        }
        public virtual void Open(Action onCompleted)
        {
            gameObject.SetActive(true);
            onCompleted?.Invoke();
        }
        public virtual void Close(Action onCompleted)
        {
            gameObject.SetActive(false);
            onCompleted?.Invoke();
        } 
        public virtual void OnBind(TViewModel viewModel) { }
    }
}
