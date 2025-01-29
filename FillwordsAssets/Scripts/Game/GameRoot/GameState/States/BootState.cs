using FillWords.Root.ServiceInterfaces;
using FillWords.Root.UI;
using FillWords.Root.UI.LoadingScreen;
using FillWords.Root.GameState.States.Params;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FillWords.Root.GameState.States
{
    public class BootState : IState
    {
        readonly GameStateMachine gameStateMachine;
        readonly List<IPrewarmableService> prewarmServices;
        readonly IUIFactory uiFactory;
        const float exitLoadingPercent = 0.1f;
        public BootState(GameStateMachine _gameStateMachine, List<IPrewarmableService> _prewarmServices, IUIFactory _uiFactory)
        {
            gameStateMachine = _gameStateMachine;
            prewarmServices = _prewarmServices;
            uiFactory = _uiFactory;
        }
        public async void Enter()
        {
            await WarmUpServices();
            LoadingScreenBase loadingScreen = await PrepareLoadingScr();
            gameStateMachine.EnterState<DataPreparingState, DataPreparingParam>(new DataPreparingParam(loadingScreen, exitLoadingPercent));
        }
        async Task WarmUpServices()
        {
            foreach(IPrewarmableService prewarmableService in prewarmServices)
            {
                await prewarmableService.Prewarm();
            }
        }
        async Task<LoadingScreenBase> PrepareLoadingScr()
        {
            LoadingScreenBase loadingScreen = await uiFactory.CreateMainLoadScreen();
            loadingScreen.UpdateProgress(exitLoadingPercent);
            return loadingScreen;
        }

        public void Exit()
        {
        }
    }
}
