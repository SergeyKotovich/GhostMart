using System;
using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Customer
{
    public class AtCashRegisterState : MonoBehaviour, IState
    {
        private CashRegister _cashRegister;
        
        private ICustomer _customer;
        private StateMachine _stateMachine;
        private bool _isMoving;
        private bool _isLeft;
        private Vector3 _currentPosition;
        private bool _isActive;

        private void Awake()
        {
            _customer = GetComponent<ICustomer>();
        }
        
        void Update()
        {
            if (_cashRegister == null || !_isActive)
            {
                return;
            }
            if (_cashRegister.IsAvailable)
            {
                EnterPayingProductsState();
            }
        }
        
        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {
            _cashRegister = (CashRegister)_customer.ShoppingList[_customer.CurrentPathIndex].StopPoint;
            _isActive = true;
            EnterPayingProductsState();
        }

        private void EnterPayingProductsState()
        {
            Debug.Log("customer position = " + _customer.PositionInLine);
            Debug.Log("paying position = " + _cashRegister.PointForCustomers.position);
            Debug.Log("transform id = " + transform.GetInstanceID());
            
            if (_customer.PositionInLine == _cashRegister.PointForCustomers.position)
            {
               // _stateMachine.Enter<PayingProductsState>();
               _isActive = false;

                _cashRegister.OnCustomerLeft(_customer);
                _customer.SetDestination(new Vector3(5,5,5));
            }
        }
    }
}