using FillWords.Root.GameState.States.Params;
using FillWords.Root.AssetManagment;
using FillWords.MainMenu.Root;
using FillWords.MainMenu;
using FillWords.Root.UI.LoadingScreen;
using FillWords.Gameplay.Root;
using FillWords.Gameplay;
using FillWords.Gameplay.Game.Root;
using System.Threading.Tasks;
using UnityEngine;

namespace FillWords.Root.GameState.States
{
    class AssetsLoadingState : IParamState<AssetLoadingParams>
    {
        readonly GameStateMachine gameStateMachine;
        readonly IAssetProvider assetProvider;
        const float stateLoadingPercent = 0.3f;
        public AssetsLoadingState(GameStateMachine _gameStateMachine, IAssetProvider _assetProvider)
        {
            gameStateMachine = _gameStateMachine;
            assetProvider = _assetProvider;
        }
        public async void Enter(AssetLoadingParams param)
        {
            await LoadAssets();
            param.LoadingScreen.UpdateProgress(param.LoadedPercent + stateLoadingPercent);
            gameStateMachine.EnterState<GameLoadingState, GameLoadingParam>(new GameLoadingParam(param.LoadingScreen,
               param.LoadedPercent + stateLoadingPercent));
        }
        async Task LoadAssets()
        {
            await assetProvider.LoadPrefab<Canvas>(AssetsKeys.UIRootKey);
            await assetProvider.LoadPrefab<MainLoadingScreen>(AssetsKeys.MainLoadScreenKey);
            await assetProvider.LoadPrefab<MainMenuViewBase>(AssetsKeys.MainMenuKey);
            await assetProvider.LoadPrefab<HomeTabViewBase>(AssetsKeys.MenuHomeTabKey);
            await assetProvider.LoadPrefab<SettingsTabViewBase>(AssetsKeys.SettingsTabKey);
            await assetProvider.LoadPrefab<GameTabViewBase>(AssetsKeys.GameViewKey);
            await assetProvider.LoadPrefab<GameFieldViewBase>(AssetsKeys.GameFieldViewKey);
            await assetProvider.LoadPrefab<MiniSettingsViewBase>(AssetsKeys.MiniSettingsKey);
            await assetProvider.LoadPrefab<VictoryTabViewBase>(AssetsKeys.VictoryViewKey);
            await assetProvider.LoadPrefab<GameObject>(AssetsKeys.CellConnectionKey); 

        }
        public void Exit()
        {

        }
    }
}
