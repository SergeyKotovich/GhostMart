using System.Collections;
using Events;
using Interfaces;
using UnityEngine;

namespace Customer
{
    public class PayingProductsState : MonoBehaviour, IState
    {
        private StateMachine _stateMachine;
        private ICustomer _customer;
        
        //TODO: must be interface
        private CashRegister _cashRegister;

        private void Awake()
        {
            _customer = GetComponent<ICustomer>();
        }
        
        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {
            StartCoroutine(PayForProducts());
            for (int i = 0; i < _customer.ShoppingList.Count; i++)
            {
                if (_customer.ShoppingList[i].StopPoint.TypeInteractablePoint == TypeInteractablePoints.CashRegister)
                {
                    _cashRegister = (CashRegister)_customer.ShoppingList[i].StopPoint;
                }
            }
        }

        private IEnumerator PayForProducts()
        {
            yield return new WaitForSeconds(2);
            
            _cashRegister.SellProducts(_customer.GetBoughtProducts());
            
            EventStreams.Global.Publish(new CustomerLeftEvent());
            _cashRegister.Queue.OnCustomerLeft(_customer);
            _stateMachine.Enter<MovingToTargetState>();
        }
    }
}