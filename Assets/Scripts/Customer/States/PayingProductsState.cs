using System.Collections;
using Interfaces;
using UnityEngine;

namespace Customer
{
    public class PayingProductsState : MonoBehaviour, IState
    {
        private StateMachine _stateMachine;
        private ICustomer _customer;

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
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(5);
            GoToExit();
        }

        private void GoToExit()
        {
            var cashRegister = (CashRegister)_customer.ShoppingList[_customer.CurrentPathIndex].StopPoint;

            cashRegister.OnCustomerLeft(_customer);
            _customer.SetDestination(new Vector3(8,0,60));
        }
    }
}