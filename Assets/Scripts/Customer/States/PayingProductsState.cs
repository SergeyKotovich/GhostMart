using System.Collections;
using Interfaces;
using UnityEngine;

namespace Customer
{
    public class PayingProductsState : MonoBehaviour, IState
    {
        [SerializeField] private CustomersController _customersController;
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
            StartCoroutine(Timer());
            _cashRegister = (CashRegister)_customer.ShoppingList[_customer.CurrentPathIndex].StopPoint;
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(2);
            var basket = (CustomerBasket)_customer.Basket;
            _cashRegister.SellProducts(basket.GetTotalProductPrice());
            GoToExit();
        }

        private void GoToExit()
        {
            //TODO: replace by using tire (Publish - CustomerLeft)
            _cashRegister.OnCustomerLeft(_customer);
            //_customersController.OnCustomerLeft();
            
            _customer.SetDestination(new Vector3(8,0,60));
        }
    }
}