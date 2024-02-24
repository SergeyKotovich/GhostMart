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
        if (!_assistant.WorkerBasket.IsEmpty())
        {
            SetProductOnStand();
        }
    }

    private void SetProductOnStand()
    {
        while (!_assistant.WorkerBasket.IsEmpty())
        {
            var product = _assistant.WorkerBasket.GetProduct();
            _stand.SetProductOnStand(product);
        }

        if (_assistant.WorkerBasket.IsEmpty())
        {
            _stateMachine.Enter<AssistantMovingToTargetState>();
        }
        
    }
    
}
