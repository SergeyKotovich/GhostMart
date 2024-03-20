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
        
        private void Update()
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
            for (int i = 0; i < _customer.OrdersList.Count; i++)
            {
                if (_customer.OrdersList[i].Target.Type == InteractableTypes.CashRegister)
                {
                    _cashRegister = (CashRegister)_customer.OrdersList[i].Target;
                }
            }
            _isActive = true;
            EnterPayingProductsState();
        }

        private void EnterPayingProductsState()
        {
            if (_customer.PositionInLine == _cashRegister.PointForCustomers.position && _cashRegister.IsAvailable)
            {
               _stateMachine.Enter<PayingProductsState, ICashRegister>(_cashRegister);
               _isActive = false;
            }
        }
    }
}