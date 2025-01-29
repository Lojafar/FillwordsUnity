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
    public class UIFactory : IUIFactory
    {
        IAssetProvider assetProvider;
        readonly DiContainer container;

        Canvas uiRoot;

        const string UIRootKey = "UIROOT";
        const string MainLoadScreenKey = "MainLoadingScreen";
        const string MainMenuKey = "MainMenu";
        const string MenuHomeTabKey = "HomeTab";
        const string SettingsTabKey = "SettingsTab";
        const string GameViewKey = "Gameplay";
        const string GameFieldViewKey = "GameField";
        const string MiniSettingsKey = "MiniSettings";
        const string VictoryViewKey = "VictoryPanel";
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
            Canvas uiRootPrefab = await assetProvider.LoadPrefab<Canvas>(UIRootKey);
            uiRoot = Object.Instantiate(uiRootPrefab);
            Object.DontDestroyOnLoad(uiRoot);
        }
        public async Task<LoadingScreenBase> CreateMainLoadScreen()
        {
            MainLoadingScreen loadingPrefab = await assetProvider.LoadPrefab<MainLoadingScreen>(MainLoadScreenKey);
            return CreateUI(loadingPrefab);
        }
        public async Task<MainMenuViewBase> CreateMainMenuView()
        {
            MainMenuViewBase mainMenuPrefab = await assetProvider.LoadPrefab<MainMenuViewBase>(MainMenuKey);
            return CreateUI(mainMenuPrefab);
        }
        public async Task<HomeTabViewBase> CreateHomeTabView()
        {
            HomeTabViewBase homeTabPrefab = await assetProvider.LoadPrefab<HomeTabViewBase>(MenuHomeTabKey);
            return CreateUI(homeTabPrefab);
        }
        public async Task<SettingsTabViewBase> CreateSettingsTabView()
        {
            SettingsTabViewBase settingsTabPrefab = await assetProvider.LoadPrefab<SettingsTabViewBase>(SettingsTabKey);
            return CreateUI(settingsTabPrefab);
        }
        public async Task<GameTabViewBase> CreateGameTabView()
        {
            GameTabViewBase gameViewPrefab = await assetProvider.LoadPrefab<GameTabViewBase>(GameViewKey);
            return CreateUI(gameViewPrefab);
        }
        public async Task<GameFieldViewBase> CreateGameFieldView()
        {
            GameFieldViewBase gameFieldViewPrefab = await assetProvider.LoadPrefab<GameFieldViewBase>(GameFieldViewKey);
            return CreateUI(gameFieldViewPrefab);
        }
        public async Task<MiniSettingsViewBase> CreateMiniSettingsView()
        {
            MiniSettingsViewBase miniSettingsPrefab = await assetProvider.LoadPrefab<MiniSettingsViewBase>(MiniSettingsKey);
            return CreateUI(miniSettingsPrefab);
        }
        public async Task<VictoryTabViewBase> CreateVictoryTabView()
        {
            VictoryTabViewBase victoryTabPrefab = await assetProvider.LoadPrefab<VictoryTabViewBase>(VictoryViewKey);
            return CreateUI(victoryTabPrefab);
        }
        T CreateUI<T>(T prefab) where T : MonoBehaviour
        {
            return Object.Instantiate(prefab, uiRoot.transform, false);
        }
    }
}
