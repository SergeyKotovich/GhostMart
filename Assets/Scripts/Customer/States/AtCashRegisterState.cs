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
            if (_customer.PositionInLine == _cashRegister.PointForCustomers.position && _cashRegister.IsAvailable)
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
            if (_customer.PositionInLine == _cashRegister.PointForCustomers.position && _cashRegister.IsAvailable)
            {
               _stateMachine.Enter<PayingProductsState>();
               _isActive = false;
            }
        }
    }
}