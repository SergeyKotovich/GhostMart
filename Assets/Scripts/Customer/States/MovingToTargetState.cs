using Events;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Customer
{
    public class MovingToTargetState : MonoBehaviour, IState
    {
        private ICustomer _customer;
        private StateMachine _stateMachine;

        private int _currentPathIndex;
        private bool _isActive;
        private bool _isInQueue;
        private IOrder _currentOrder;

        private void Awake()
        {
            _customer = GetComponent<ICustomer>();
        }

        private void Update()
        {
            if (!_isActive) return;
            if (_customer.IsAtTargetPoint())
            {
                if (_currentOrder.TargetType == InteractableTypes.CashRegister)
                {
                    EnterAtCashRegisterState();
                }
                else if (_currentOrder.TargetType == InteractableTypes.Stand)
                {
                    EnterGettingProductsState();
                }
                
                else if (_currentOrder.TargetType == InteractableTypes.Exit)
                {
                    EventStreams.Global.Publish(new CustomerLeftEvent());
                    _isActive = false;
                }
                
                else
                {
                    _isActive = false;
                }
            }
        }
        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void OnEnter()
        {
            MoveToNextPoint();
            _isActive = true;
        }

        private void MoveToNextPoint()
        {
            var orderList = _customer.OrdersList;
            _currentOrder = orderList[_currentPathIndex];
            EventStreams.Global.Publish(new OrderUpdatedEvent(_currentOrder, transform));

            if (_currentOrder.TargetType == InteractableTypes.CashRegister)
            {
                MoveToCashRegister();
                _currentPathIndex++;
                return;
            }
            
            if (_currentPathIndex < orderList.Count)
            {
                var destination = _currentOrder.TargetPosition;
                _customer.SetDestination(destination);
                if (_currentOrder.TargetType == InteractableTypes.Exit)
                {
                    _customer.SetDestination(_currentOrder.TargetPosition);
                }
                _currentPathIndex++;
            }
        }

        private void MoveToCashRegister()
        {
            var cashRegister = (ICashRegister)_customer.OrdersList[_currentPathIndex].Target;
            var destination = cashRegister.GetFreePosition(_customer);
            _customer.SetDestination(destination);
            _isInQueue = true;
        }
        
        private void EnterGettingProductsState()
        {
            _stateMachine.Enter<GettingProductsState, IOrder>(_currentOrder);
            _isActive = false;
        }
        
        private void EnterAtCashRegisterState()
        {
            _stateMachine.Enter<AtCashRegisterState>();
            _isActive = false;
        }

    }
}