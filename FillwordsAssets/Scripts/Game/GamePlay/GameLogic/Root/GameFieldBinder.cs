using FillWords.Root.UI;
using FillWords.Root.Data;
using FillWords.Gameplay.Root;
using FillWords.Gameplay.Level;
using Zenject;
namespace FillWords.Gameplay.Game.Root
{
    public class GameFieldBinder
    {
        readonly GameTabModel gameTabModel;
        GameFieldViewBase gameFieldView;

        readonly DiContainer diContainer;
        readonly ILevelLoader levelLoader;
        readonly AllDataContainer allDataContainer;
        public GameFieldBinder(GameTabModel _gameTabModel, DiContainer _diContainer)
        {
            gameTabModel = _gameTabModel;
            diContainer = _diContainer;
            levelLoader = diContainer.Resolve<ILevelLoader>();
            allDataContainer = diContainer.Resolve<AllDataContainer>();
            CreateView(diContainer.Resolve<IUIFactory>());
            Bind();
        }
        async void CreateView(IUIFactory uiFactory)
        {
            gameFieldView = await uiFactory.CreateGameFieldView();
            diContainer.Inject(gameFieldView);
        }
        void Bind()
        {
            GameFieldModel gameFieldModel = new(gameTabModel, levelLoader, allDataContainer);
            GameFieldViewModel gameFieldVM = new(gameFieldModel);
            gameFieldView.Bind(gameFieldVM);
            gameTabModel.RegisterTab(gameFieldModel);
        }
    }
}
