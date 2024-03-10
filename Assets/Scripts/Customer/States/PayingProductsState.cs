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
            _cashRegister = (CashRegister)_customer.ShoppingList[_customer.CurrentPathIndex].StopPoint;
        }

        private IEnumerator PayForProducts()
        {
            yield return new WaitForSeconds(2);
            var basket = (CustomerBasket)_customer.Basket;
            _cashRegister.SellProducts(basket.GetTotalProductPrice());
            
            _customer.CurrentPathIndex++;
            EventStreams.Global.Publish(new CustomerLeftEvent());
            _cashRegister.Queue.OnCustomerLeft(_customer);
            _stateMachine.Enter<MovingToTargetState>();
        }
    }
}