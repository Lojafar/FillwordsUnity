using FillWords.Root.GameState.States;
using FillWords.Utils;
using System;
using System.Collections.Generic;
using Zenject;
namespace FillWords.Root.GameState
{
    public class GameStateMachine : IInitializable
    {
        readonly IGameStatesFactory gameStatesFactory;
        readonly Dictionary<Type, IExitableState> statesMap;
        IExitableState currentState;
        public GameStateMachine(IGameStatesFactory _gameStatesFactory)
        {
            gameStatesFactory = _gameStatesFactory;
            statesMap = new();
        }
        public void Initialize()
        {
            statesMap.Add(typeof(BootState), gameStatesFactory.CreateState<BootState>());
            statesMap.Add(typeof(DataPreparingState), gameStatesFactory.CreateState<DataPreparingState>());
            statesMap.Add(typeof(GameLoadingState), gameStatesFactory.CreateState<GameLoadingState>());
            statesMap.Add(typeof(GameplayState), gameStatesFactory.CreateState<GameplayState>());
           
            EnterState<BootState>();
        }
        public void EnterState<T>() where T : class, IState
        {
            ChangeState<T>().Enter();
        }
        public void EnterState<TState, TParam>(TParam param) where TState : class, IParamState<TParam>
        {
            ChangeState<TState>().Enter(param);
        }
        T ChangeState<T>() where T : class, IExitableState
        {
            currentState?.Exit();
            currentState = GetState<T>();
            DebugUtil.Log($"GSM state changed to {typeof(T)}");
            return currentState as T;
        }
        T GetState<T>() where T : class, IExitableState
        {
            return statesMap[typeof(T)] as T;
        }
    }
}
