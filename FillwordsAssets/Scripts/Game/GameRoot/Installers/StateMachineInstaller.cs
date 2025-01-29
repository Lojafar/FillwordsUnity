using FillWords.Root.GameState;
using FillWords.Root.GameState.States;
using Zenject;

namespace FillWords.Root.Installers
{
    class StateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStates();
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle().NonLazy();
        }

        void BindStates()
        {
            Container.Bind<BootState>().AsSingle().NonLazy();
            Container.Bind<DataPreparingState>().AsSingle().NonLazy();
            Container.Bind<GameLoadingState>().AsSingle().NonLazy();
            Container.Bind<GameplayState>().AsSingle().NonLazy();
        }
    }
}
