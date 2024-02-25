using System;
using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Customer
{
    public class AtCashRegisterState : MonoBehaviour, IState
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;
        private CashRegister _cashRegister;
        
        private ICustomer _customer;
        private StateMachine _stateMachine;
        private bool _isMoving;
        private bool _isLeft;

        private void Awake()
        {
            _customer = GetComponent<ICustomer>();
        }
        
        void Update()
        {
            //if (_cashRegister == null)
            //{
            //    return;
            //}
            //if (Vector3.Distance(transform.position, _cashRegister.PointForCustomers.position) < 0.5)
            //{
            //    StartCoroutine(Timer());
            //}
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(5);
            _cashRegister.OnCustomerLeft();
            _isLeft = true;
        }
        
        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {
            _cashRegister = (CashRegister)_customer.ShoppingList[_customer.CurrentPathIndex].StopPoint;
            _cashRegister.LineChanged += TryGoForward;
            StopMoving();
        }
        
      
        private void StopMoving()
        {
            _isMoving = false;
            _animator.SetBool("IsMoving", _isMoving);
        }

        private void TryGoForward()
        {
            var lastBusyPosition = _cashRegister.LastBusyPositionInLine;
            
            float distance = Vector3.Distance(transform.position, lastBusyPosition);
            if (distance > 0.5)
            {
                Debug.Log("еще двлеко");
                return;
            }
            
            Debug.Log("try go forward");
            var destination = _cashRegister.GetFreePosition();
            _navMeshAgent.SetDestination(destination);

            _isMoving = true;
            _animator.SetBool("IsMoving", _isMoving);
        }

        private void EnterPayingProductsState()
        {
            _stateMachine.Enter<PayingProductsState>();
            StopMoving();
        }

       
    }
}