using FillWords.Root.GameState.States.Params;
using FillWords.Root.AssetManagment;
using FillWords.Root.SaveLoad;
using FillWords.Root.Data;
using FillWords.Root.Data.Initial;
using System.Threading.Tasks;

namespace FillWords.Root.GameState.States
{
    public class DataPreparingState : IParamState<DataPreparingParam>
    {
        readonly GameStateMachine gameStateMachine;
        readonly ISaverLoader saverLoader;
        readonly AllDataContainer allDataContainer;
        readonly IInitDataLoader initDataLoaderBase;
        const float stateLoadingPercent = 0.3f;
        public DataPreparingState(GameStateMachine _gameStateMachine, ISaverLoader _saverLoader, AllDataContainer _allDataContainer, IInitDataLoader _initDataLoaderBase)
        {
            gameStateMachine = _gameStateMachine;
            saverLoader = _saverLoader;
            allDataContainer = _allDataContainer;
            initDataLoaderBase = _initDataLoaderBase;
        }
        public async void Enter(DataPreparingParam stateParam)
        {
            await LoadPlayerData();
            stateParam.LoadingScreen.UpdateProgress(stateParam.LoadedPercent + stateLoadingPercent);
            gameStateMachine.EnterState<GameLoadingState, GameLoadingParam>(new GameLoadingParam(stateParam.LoadingScreen,
                stateParam.LoadedPercent + stateLoadingPercent));
        }
        public async Task LoadPlayerData()
        {
            ProgressData progressData = await saverLoader.LoadProgress();
            SettingsData settingsData = await saverLoader.LoadSettings();
            if (progressData == null) progressData = initDataLoaderBase.LoadInitProgress(); 
            if (settingsData == null) settingsData = initDataLoaderBase.LoadInitSettings();
            allDataContainer.ProgressData = new ProgressDataProxy(progressData);
            allDataContainer.SettingsData = new SettingsDataProxy(settingsData);
        }
        public void Exit()
        {
        }
    }
}
