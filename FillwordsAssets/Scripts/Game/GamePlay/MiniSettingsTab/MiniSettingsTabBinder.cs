using FillWords.Root.UI;
using FillWords.Root.SaveLoad;
using FillWords.Root.Data;
using FillWords.Gameplay.Root;

namespace FillWords.Gameplay
{
    class MiniSettingsTabBinder
    {
        MiniSettingsViewBase settingsTabView;
        readonly GameTabModel gameTabModel;
        readonly AllDataContainer allDataContainer;
        readonly ISaverLoader saverLoader;
        public MiniSettingsTabBinder(GameTabModel _gameTabModel, IUIFactory uiFactory, AllDataContainer _allDataContainer, ISaverLoader _saverLoader)
        {
            gameTabModel = _gameTabModel;
            allDataContainer = _allDataContainer;
            saverLoader = _saverLoader;
            CreateView(uiFactory);
            Bind();
        }
        async void CreateView(IUIFactory uiFactory)
        {
            settingsTabView = await uiFactory.CreateMiniSettingsView();
        }
        void Bind()
        {
            MiniSettingsTabModel settingsTabModel = new(gameTabModel, allDataContainer, saverLoader);
            MiniSettingsTabVM settingsTabVM = new(settingsTabModel);
            settingsTabView.Bind(settingsTabVM);
            gameTabModel.RegisterTab(settingsTabModel);
        }
    }
}
