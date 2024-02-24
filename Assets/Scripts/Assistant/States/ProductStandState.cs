using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ProductStandState : MonoBehaviour, IState
{
    private ICollectable _assistant;
    [SerializeField] private Stand _stand;
    private StateMachine _stateMachine;

    private void Awake()
    {
        _assistant = GetComponent<ICollectable>();
    }

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        if (!_assistant.Basket.IsEmpty())
        {
            SetProductOnStand();
        }
    }

    private void SetProductOnStand()
    {
        while (!_assistant.Basket.IsEmpty())
        {
            var product = _assistant.Basket.GetProduct();
            _stand.SetProductOnStand(product);
        }

        if (_assistant.Basket.IsEmpty())
        {
            _stateMachine.Enter<AssistantMovingToTargetState>();
        }
        
    }

    public void OnExit()
    {
        
    }
}
