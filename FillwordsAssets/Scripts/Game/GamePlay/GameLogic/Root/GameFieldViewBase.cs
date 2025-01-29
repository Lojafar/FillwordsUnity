using FillWords.Root.UI.Tabs;
using R3;
namespace FillWords.Gameplay.Game.Root
{
    public class GameFieldViewBase : TabViewBase<GameFieldViewModel>
    {
        public override void OnBind(GameFieldViewModel viewModel)
        {
            viewModel.OpeningEvent.Subscribe(onCompleted => Open(onCompleted));
            viewModel.ClosingEvent.Subscribe(onCompleted => Close(onCompleted));
        }
    }
}
