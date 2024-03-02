using System;
using System.Collections.Generic;
using Interfaces;
using Unity.VisualScripting;

public class StateMachine
{
    private readonly Dictionary<Type, IInitializable> _states = new();
    private IInitializable _currentState;

    public StateMachine(params IInitializable[] states)
    {
        foreach (var state in states)
        {
            _states[state.GetType()] = state;
        }
    }
    
    
    public void Initialize()
    {
        foreach (var statePairs in _states)
        {
            statePairs.Value.Initialize(this);
        }
    }
    
    public void Enter<TState>() where TState : IState
    {
        var currentState = (IState) _states[typeof(TState)];
      //  _currentState =(IState) _states[typeof(TState)];
        currentState.OnEnter();
    }
    public void Enter<TState,TPayload>(TPayload payload) where TState : class, IPayLoadedState<TPayload>
    {
        var currentState = (IPayLoadedState<TPayload>) _states[typeof(TState)];
      //  _currentState = _states[typeof(TState)];
        currentState.OnEnter(payload);
    }
    
}