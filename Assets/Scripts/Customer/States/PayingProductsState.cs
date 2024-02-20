using Interfaces;
using UnityEngine;

namespace Customer
{
    public class PayingProductsState : MonoBehaviour, IState
    {
        private StateMachine _stateMachine;

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}