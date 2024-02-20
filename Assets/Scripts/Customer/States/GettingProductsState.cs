
using System;
using UnityEngine;

namespace Customer
{
    public class GettingProductsState : MonoBehaviour, IState
    {
        [SerializeField] private Customer _customer;
        
        private StateMachine _stateMachine;

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {
            _customer.GotProducts += EnterMovingToTargetState;
            _customer.TakeProducts();
        }

        public void OnExit()
        {
            _customer.GotProducts -= EnterMovingToTargetState;
        }
        
        private void EnterMovingToTargetState()
        {
            _stateMachine.Enter<MovingToTargetState>();
        }
    }
}