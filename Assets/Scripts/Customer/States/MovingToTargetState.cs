
using UnityEngine;

namespace Customer
{
    public class MovingToTargetState : MonoBehaviour, IState
    {
        [SerializeField] private Customer _customer;
        private StateMachine _stateMachine;
        
        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {
            Debug.Log("MovingToTargetState");

            _customer.CameToTarget += EnterGettingProductsState;
            _customer.MoveToNextPoint();
        }

        private void EnterGettingProductsState()
        {
            // in waitingState customer has to wait until they get all products they need
            // and than starts moving to a next point
            
            _stateMachine.Enter<GettingProductsState>();
            _customer.StopMoving();
        }
        
        public void OnExit()
        {
            _customer.CameToTarget -= EnterGettingProductsState;
        }
    }
}