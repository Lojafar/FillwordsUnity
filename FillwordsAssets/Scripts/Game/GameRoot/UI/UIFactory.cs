using FillWords.Root.AssetManagment;
using FillWords.Root.UI.LoadingScreen;
using FillWords.MainMenu.Root;
using FillWords.MainMenu;
using FillWords.Gameplay.Root;
using FillWords.Gameplay;
using FillWords.Gameplay.Game.Root;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
namespace FillWords.Root.UI
{
    class UIFactory : IUIFactory
    {
        IAssetProvider assetProvider;
        readonly DiContainer container;

        Canvas uiRoot;

        public UIFactory(DiContainer _container)
        {
            container = _container;
        }
        public async Task Prewarm()
        {
            assetProvider = container.Resolve<IAssetProvider>();
            await CreateUIRoot();
        }
        async Task CreateUIRoot()
        {
            Canvas uiRootPrefab = await assetProvider.LoadPrefab<Canvas>(AssetsKeys.UIRootKey);
            uiRoot = Object.Instantiate(uiRootPrefab);
            Object.DontDestroyOnLoad(uiRoot);
        }
        public async Task<LoadingScreenBase> CreateMainLoadScreen()
        {
            MainLoadingScreen loadingPrefab = await assetProvider.LoadPrefab<MainLoadingScreen>(AssetsKeys.MainLoadScreenKey);
            return CreateUI(loadingPrefab);
        }
        public async Task<MainMenuViewBase> CreateMainMenuView()
        {
            MainMenuViewBase mainMenuPrefab = await assetProvider.LoadPrefab<MainMenuViewBase>(AssetsKeys.MainMenuKey);
            return CreateUI(mainMenuPrefab);
        }
        public async Task<HomeTabViewBase> CreateHomeTabView()
        {
            HomeTabViewBase homeTabPrefab = await assetProvider.LoadPrefab<HomeTabViewBase>(AssetsKeys.MenuHomeTabKey);
            return CreateUI(homeTabPrefab);
        }
        public async Task<SettingsTabViewBase> CreateSettingsTabView()
        {
            SettingsTabViewBase settingsTabPrefab = await assetProvider.LoadPrefab<SettingsTabViewBase>(AssetsKeys.SettingsTabKey);
            return CreateUI(settingsTabPrefab);
        }
        public async Task<GameTabViewBase> CreateGameTabView()
        {
            GameTabViewBase gameViewPrefab = await assetProvider.LoadPrefab<GameTabViewBase>(AssetsKeys.GameViewKey);
            return CreateUI(gameViewPrefab);
        }
        public async Task<GameFieldViewBase> CreateGameFieldView()
        {
            GameFieldViewBase gameFieldViewPrefab = await assetProvider.LoadPrefab<GameFieldViewBase>(AssetsKeys.GameFieldViewKey);
            return CreateUI(gameFieldViewPrefab);
        }
        public async Task<MiniSettingsViewBase> CreateMiniSettingsView()
        {
            MiniSettingsViewBase miniSettingsPrefab = await assetProvider.LoadPrefab<MiniSettingsViewBase>(AssetsKeys.MiniSettingsKey);
            return CreateUI(miniSettingsPrefab);
        }
        public async Task<VictoryTabViewBase> CreateVictoryTabView()
        {
            VictoryTabViewBase victoryTabPrefab = await assetProvider.LoadPrefab<VictoryTabViewBase>(AssetsKeys.VictoryViewKey);
            return CreateUI(victoryTabPrefab);
        }
        T CreateUI<T>(T prefab) where T : MonoBehaviour
        {
            return Object.Instantiate(prefab, uiRoot.transform, false);
        }
    }
}
