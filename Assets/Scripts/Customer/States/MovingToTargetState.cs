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
        
        private void Awake()
        {
            _customer = GetComponent<ICustomer>();
        }

        private void Update()
        {
            if (!_isActive) return;
            if (_customer.IsAtTargetPoint())
            {
                if (_customer.CurrentTargetType == TypeInteractablePoints.CashRegister)
                {
                    EnterAtCashRegisterState();
                }
                else if (_customer.CurrentTargetType == TypeInteractablePoints.Stand)
                {
                    EnterGettingProductsState();
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
            var shoppingList = _customer.ShoppingList;
            _customer.SetCurrentTargetType(shoppingList[_currentPathIndex].StopPoint.TypeInteractablePoint);

            if (shoppingList[_currentPathIndex].StopPoint.TypeInteractablePoint == TypeInteractablePoints.CashRegister)
            {
                MoveToCashRegister();
                _currentPathIndex++;
                return;
            }
            
            if (_currentPathIndex < shoppingList.Count)
            {
                var destination = shoppingList[_currentPathIndex].Position;
                _customer.SetDestination(destination);

                if (shoppingList[_currentPathIndex].StopPoint.TypeInteractablePoint == TypeInteractablePoints.Exit)
                {
                    //_customer._productBarView.UpdateProductBar(shoppingList[currentPathIndex].StopPoint.StandIcon);
                    _customer.SetDestination(shoppingList[_currentPathIndex].StopPoint.PointForCustomers.position);
                }
                _currentPathIndex++;
                // _customer._productBarView.UpdateProductBar(shoppingList[currentPathIndex]);
            }
        }

        private void MoveToCashRegister()
        {
            var cashRegister = (CashRegister)_customer.ShoppingList[_currentPathIndex].StopPoint;

            var destination = cashRegister.Queue.GetFreePosition(_customer);
            _customer.SetDestination(destination);

            //_customer._productBarView.UpdateProductBar(cashRegister.StandIcon);
            _isInQueue = true;
        }
        
        private void EnterGettingProductsState()
        {
            _stateMachine.Enter<GettingProductsState>();
            _isActive = false;
        }
        
        private void EnterAtCashRegisterState()
        {
            _stateMachine.Enter<AtCashRegisterState>();
            _isActive = false;
        }

    }
}