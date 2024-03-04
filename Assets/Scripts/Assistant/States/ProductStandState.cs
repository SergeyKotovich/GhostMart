using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ProductStandState : MonoBehaviour, IPayLoadedState<IStand>
{
    private IWorker _assistant;
   // [SerializeField] private Stand _stand;
    private StateMachine _stateMachine;
    private IStand _stand;

    private void Awake()
    {
        _assistant = GetComponent<IWorker>();
    }

    public void OnEnter(IStand payload)
    {
        _stand = payload;
        if (!_assistant.Basket.IsEmpty())
        {
            if (_stand.IsFull())
            {
                _stateMachine.Enter<RecyclingProductsState>();
            }
            else
            {
                SetProductOnStand();
            }
            
        }
        
    }

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
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
}