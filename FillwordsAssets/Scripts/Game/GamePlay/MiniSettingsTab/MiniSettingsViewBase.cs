using FillWords.Root.UI.Tabs;
using R3;
namespace FillWords.Gameplay
{
    public class MiniSettingsViewBase : TabViewBase<MiniSettingsTabVM>
    {
        public override void OnBind(MiniSettingsTabVM viewModel)
        {
            viewModel.OpeningEvent.Subscribe(onCompleted => Open(onCompleted));
            viewModel.ClosingEvent.Subscribe(onCompleted => Close(onCompleted));
        }
    }
}
