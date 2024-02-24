using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class StateMachine
{
    private readonly Dictionary<Type, IState> _states = new();
    private IState _currentState;

    public StateMachine(params IState[] states)
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
        _currentState = _states[typeof(TState)];
        _currentState.OnEnter();
    }
}