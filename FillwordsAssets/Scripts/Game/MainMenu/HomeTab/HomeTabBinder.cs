using FillWords.Root.UI;
using FillWords.Root.Data;
using FillWords.MainMenu.Root;

namespace FillWords.MainMenu
{
    public class HomeTabBinder 
    {
        HomeTabViewBase homeTabView;
        readonly MainMenuModel mainMenuModel;
        readonly AllDataContainer allDataContainer;
        readonly TabsHandler tabsHandler;
        public HomeTabBinder(MainMenuModel _mainMenuModel, IUIFactory uiFactory, AllDataContainer _allDataContainer, TabsHandler _tabsHandler)
        {
            mainMenuModel = _mainMenuModel;
            allDataContainer = _allDataContainer;
            tabsHandler = _tabsHandler;
            CreateView(uiFactory);
            Bind();
        }
        async void CreateView(IUIFactory uiFactory)
        {
            homeTabView = await uiFactory.CreateHomeTabView();
        }
        void Bind()
        {
            HomeTabModel homeTabModel = new(allDataContainer, tabsHandler);
            HomeTabViewModel homeTabVM = new(homeTabModel);
            homeTabView.Bind(homeTabVM);
            mainMenuModel.RegisterTab(homeTabModel);
        }
    }
}
