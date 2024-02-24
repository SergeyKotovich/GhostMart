using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Customer
{
    public class MovingToTargetState : MonoBehaviour, IState
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;
        
        private ICustomer _customer;
        private StateMachine _stateMachine;
        private bool _isMoving;

        private void Awake()
        {
            _customer = GetComponent<ICustomer>();
        }

        void Update()
        {
            if (_navMeshAgent.remainingDistance < 1f && !_navMeshAgent.pathPending)
            {
                EnterGettingProductsState();
            }
        }
        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void OnEnter()
        {
            MoveToNextPoint();
        }
        private void MoveToNextPoint()
        {
            var shoppingList = _customer.ShoppingList;
            var currentPathIndex = _customer.CurrentPathIndex;
            
            if (currentPathIndex < shoppingList.Count)
            {
                _navMeshAgent.SetDestination(shoppingList[currentPathIndex].Position);
                
                _isMoving = true;
                _animator.SetBool("IsMoving", _isMoving);
                _customer._productBarView.UpdateProductBar(shoppingList[currentPathIndex]);
                
            }
            else
            {
                _navMeshAgent.isStopped = true;
                _animator.SetBool("IsMoving", false);
            }
        }

        private void StopMoving()
        {
            _isMoving = false;
            _animator.SetBool("IsMoving", _isMoving);
        }
        

        private void EnterGettingProductsState()
        {
            _stateMachine.Enter<GettingProductsState>();
            StopMoving();
        }

    }
}