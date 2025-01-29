using FillWords.Root.UI;
using FillWords.Root.Data;
using FillWords.Root.SaveLoad;
using FillWords.Gameplay.Root;
namespace FillWords.Gameplay
{
    class VictoryTabBinder 
    {
        readonly GameTabModel gameTabModel;
        VictoryTabViewBase victoryTabView;
        readonly AllDataContainer allDataContainer;
        readonly ISaverLoader saverLoader;
        public VictoryTabBinder(GameTabModel _gameTabModel, IUIFactory uiFactory, AllDataContainer _allDataContainer, ISaverLoader _saverLoader)
        {
            gameTabModel = _gameTabModel;
            allDataContainer = _allDataContainer;
            saverLoader = _saverLoader;
            CreateView(uiFactory);
            Bind();
        }
        async void CreateView(IUIFactory uiFactory)
        {
            victoryTabView = await uiFactory.CreateVictoryTabView();
        }
        void Bind()
        {
            VictoryTabModel victoryTabModel = new(gameTabModel, allDataContainer, saverLoader);
            VictoryTabViemModel victoryTabVM = new(victoryTabModel);
            victoryTabView.Bind(victoryTabVM);
            gameTabModel.RegisterTab(victoryTabModel);
        }
    }
}
