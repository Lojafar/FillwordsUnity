using FillWords.Root.GameState.States;
namespace FillWords.Root.GameState
{
    public interface IGameStatesFactory 
    {
        public T CreateState<T>() where T : IExitableState;
    }
}
