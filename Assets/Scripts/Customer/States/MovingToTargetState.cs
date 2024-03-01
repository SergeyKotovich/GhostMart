using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Customer
{
    public class MovingToTargetState : MonoBehaviour, IState
    {
        private ICustomer _customer;
        private StateMachine _stateMachine;
        private bool _isMoving;
        private TypeInteractablePoints _currentTargetType;
        private bool _isActive;

        private void Awake()
        {
            _customer = GetComponent<ICustomer>();
        }

        private void Update()
        {
            if (!_isActive) return;
            if (_customer.IsAtTargetPoint())
            {
                if (_currentTargetType == TypeInteractablePoints.CashRegister)
                {
                    EnterAtCashRegisterState();
                }
                else
                {
                    EnterGettingProductsState();
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
            var currentPathIndex = _customer.CurrentPathIndex;
            _currentTargetType = shoppingList[currentPathIndex].StopPoint.TypeInteractablePoint;

            if (shoppingList[currentPathIndex].StopPoint.TypeInteractablePoint == TypeInteractablePoints.CashRegister)
            {
                MoveToCashRegister();
                return;
            }
            
            if (currentPathIndex < shoppingList.Count)
            {
                var destination = shoppingList[currentPathIndex].Position;
                _customer.SetDestination(destination);
                
                _isMoving = true;
                _customer._productBarView.UpdateProductBar(shoppingList[currentPathIndex]);
            }
            else
            {
                _customer.StopMoving();
            }
        }

        private void MoveToCashRegister()
        {
            var currentPathIndex = _customer.CurrentPathIndex;
            var cashRegister = (CashRegister)_customer.ShoppingList[currentPathIndex].StopPoint;

            var destination = cashRegister.GetFreePosition(_customer);
            _customer.SetDestination(destination);

            _isMoving = true;
            _customer._productBarView.UpdateProductBar(cashRegister.StandIcon);
        }
        
        
        private void EnterGettingProductsState()
        {
            _stateMachine.Enter<GettingProductsState>();
            _isActive = false;
            _customer.StopMoving();
        }
        
        private void EnterAtCashRegisterState()
        {
            _stateMachine.Enter<AtCashRegisterState>();
            _isActive = false;
            _customer.StopMoving();
        }

    }
}