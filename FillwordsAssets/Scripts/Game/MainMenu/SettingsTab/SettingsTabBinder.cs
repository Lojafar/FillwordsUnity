using FillWords.Root.UI;
using FillWords.Root.SaveLoad;
using FillWords.Root.Data;
using FillWords.MainMenu.Root;

namespace FillWords.MainMenu
{
    class SettingsTabBinder
    {
        SettingsTabViewBase settingsTabView;
        readonly MainMenuModel mainMenuModel;
        readonly AllDataContainer allDataContainer;
        readonly ISaverLoader saverLoader;
        public SettingsTabBinder(MainMenuModel _mainMenuModel, IUIFactory uiFactory, AllDataContainer _allDataContainer, ISaverLoader _saverLoader)
        {
            mainMenuModel = _mainMenuModel;
            allDataContainer = _allDataContainer;
            saverLoader = _saverLoader;
            CreateView(uiFactory);
            Bind();
        }
        async void CreateView(IUIFactory uiFactory)
        {
            settingsTabView = await uiFactory.CreateSettingsTabView();
        }
        void Bind()
        {
            SettingsTabModel settingsTabModel = new(allDataContainer, saverLoader);
            SettingsTabViewModel settingsTabVM = new(settingsTabModel);
            settingsTabView.Bind(settingsTabVM);
            mainMenuModel.RegisterTab(settingsTabModel);
        }
    }
}
