using FillWords.Root.GameState.States;
using Zenject;
namespace FillWords.Root.GameState
{
    class GameStatesFactory : IGameStatesFactory
    {
        readonly DiContainer container;
        public GameStatesFactory(DiContainer _container)
        {
            container = _container;
        }
        public T CreateState<T>() where T : IExitableState
        {
            return container.Resolve<T>();
        }
    }
}
