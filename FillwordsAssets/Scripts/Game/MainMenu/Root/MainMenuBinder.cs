using FillWords.Root.UI;
using FillWords.Root.SaveLoad;
using FillWords.Root.Data;
using Zenject;

namespace FillWords.MainMenu.Root
{
    class MainMenuBinder 
    {
        readonly DiContainer diContainer;
        MainMenuViewBase mainMenuView;
        MainMenuModel mainModel;
        readonly IUIFactory uiFactory;
        readonly TabsHandler tabsHandler;
        public MainMenuBinder(DiContainer _diContainer)
        {
            diContainer = _diContainer;
            uiFactory = diContainer.Resolve<IUIFactory>();
            tabsHandler = diContainer.Resolve<TabsHandler>();
            CreateView();
            BindView();
        }
        public async void CreateView()
        {
            mainMenuView = await uiFactory.CreateMainMenuView();
        }
        void BindView()
        {
            mainModel = new MainMenuModel();
            tabsHandler.RegisterTabModel(mainModel);
            MainMenuViewModel mainVM = new(mainModel);
            mainMenuView.Bind(mainVM);

            CreateSubTabsBinders();
        }
        void CreateSubTabsBinders()
        {
            new SettingsTabBinder(mainModel, uiFactory, 
                diContainer.Resolve<AllDataContainer>(), diContainer.Resolve<ISaverLoader>());
            new HomeTabBinder(mainModel, uiFactory,
                diContainer.Resolve<AllDataContainer>(), tabsHandler);
        }
    }
}
