using FillWords.Root.UI.Tabs;
using R3;
namespace FillWords.MainMenu
{
    public class SettingsTabViewBase : ScalingTabViewBase<SettingsTabViewModel>
    {
        public override void OnBind(SettingsTabViewModel viewModel)
        {
            viewModel.OpeningEvent.Subscribe(onCompleted => Open(onCompleted));
            viewModel.ClosingEvent.Subscribe(onCompleted => Close(onCompleted));
        }
    }
}
