using FillWords.Root.UI.Tabs;
using R3;

namespace FillWords.Gameplay 
{
    public abstract class VictoryTabViewBase : TabViewBase<VictoryTabViemModel>
    {
        public override void OnBind(VictoryTabViemModel viewModel)
        {
            viewModel.OpeningEvent.Subscribe(onCompleted => Open(onCompleted));
            viewModel.ClosingEvent.Subscribe(onCompleted => Close(onCompleted));
        }
    }
}
