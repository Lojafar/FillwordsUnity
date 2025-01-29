using FillWords.Root.UI.Tabs;
using R3;

namespace FillWords.MainMenu.Root
{
    public class MainMenuViewBase : TabViewBase<MainMenuViewModel>
    {
        public override void OnBind(MainMenuViewModel viewModel)
        {
            viewModel.OpeningEvent.Subscribe(onCompleted => Open(onCompleted));
            viewModel.ClosingEvent.Subscribe(onCompleted => Close(onCompleted));
        }
    }
}
