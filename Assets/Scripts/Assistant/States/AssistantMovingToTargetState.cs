using System;
using UnityEngine;

public class AssistantMovingToTargetState : MonoBehaviour, IState
{
    [SerializeField] private Assistant _assistant;
    private StateMachine _stateMachine;
    private bool _isProductFactory;
    private bool _isStand;

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _assistant.CameToTarget += EnterNextState;
        _assistant.MoveToPoint();
    }

    private void EnterNextState()
    {
        if (_isProductFactory)
        {
            _stateMachine.Enter<CollectingProductsState>();
        }

        if (_isStand)
        {
            _stateMachine.Enter<ProductStandState>();
        }
        
        
    }

    public void OnExit()
    {
        _assistant.CameToTarget -= EnterNextState;
        _isStand = false;
        _isProductFactory = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalConstants.PRODUCT_FACTORY_TAG))
        {
            _isProductFactory = true;
        }
        if (other.CompareTag(GlobalConstants.STAND))
        {
            _isStand = true;
        }
    }
}
