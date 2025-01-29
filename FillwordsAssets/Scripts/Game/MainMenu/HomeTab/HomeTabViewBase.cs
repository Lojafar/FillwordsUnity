using FillWords.Root.UI.Tabs;
using R3;

namespace FillWords.MainMenu
{
    public class HomeTabViewBase : ScalingTabViewBase<HomeTabViewModel>
    {
        public override void OnBind(HomeTabViewModel viewModel)
        {
            viewModel.OpeningEvent.Subscribe(onCompleted => Open(onCompleted));
            viewModel.ClosingEvent.Subscribe(onCompleted => Close(onCompleted));
        }
    }

}
