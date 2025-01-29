using FillWords.Root.UI;
using FillWords.Root.Data;
using FillWords.Root.SaveLoad;
using FillWords.Gameplay.Game.Root;
using FillWords.Gameplay.Level;
using Zenject;

namespace FillWords.Gameplay.Root
{
    public class GameplayBinder 
    {
        GameTabViewBase view;
        GameTabModel mainGameModel;

        readonly DiContainer diContainer;
        readonly IUIFactory uiFactory;
        readonly TabsHandler tabsHandler;
        readonly AllDataContainer allDataContainer;
        public GameplayBinder(DiContainer _diContainer)
        {
            diContainer = _diContainer;
            uiFactory = diContainer.Resolve<IUIFactory>();
            tabsHandler = diContainer.Resolve<TabsHandler>();
            allDataContainer = diContainer.Resolve<AllDataContainer>();
            CreateViewModel();
            BindView();
        }
        public async void CreateViewModel()
        {
            view = await uiFactory.CreateGameTabView();
        }
        public void BindView()
        {
            mainGameModel = new GameTabModel(tabsHandler, allDataContainer);
            GameTabViewModel viewModel = new(mainGameModel);
            view.Bind(viewModel);
            tabsHandler.RegisterTabModel(mainGameModel);
            CreateSubTabsBinders();
        }
        void CreateSubTabsBinders()
        {
            ISaverLoader saverLoader = diContainer.Resolve<ISaverLoader>();
            new GameFieldBinder(mainGameModel, diContainer);
            new MiniSettingsTabBinder(mainGameModel, uiFactory, allDataContainer, saverLoader);
            new VictoryTabBinder(mainGameModel, uiFactory, allDataContainer, saverLoader);
        }
    }
}