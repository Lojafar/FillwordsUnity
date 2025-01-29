using FillWords.Root.UI.Tabs;
using R3;
namespace FillWords.Gameplay.Root
{
    public abstract class GameTabViewBase : TabViewBase<GameTabViewModel>
    {
        public override void OnBind(GameTabViewModel viewModel)
        {
            viewModel.OpeningEvent.Subscribe(onCompleted => Open(onCompleted));
            viewModel.ClosingEvent.Subscribe(onCompleted => Close(onCompleted));
        }
    }
}
