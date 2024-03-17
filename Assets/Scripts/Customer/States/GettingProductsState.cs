
using System;
using System.Collections.Generic;
using DG.Tweening;
using Events;
using Interfaces;
using UnityEngine;

namespace Customer
{
    public class GettingProductsState : MonoBehaviour, IPayLoadedState<IOrder>
    {
        private ICustomer _customer;
        private StateMachine _stateMachine;
        private bool _isTakingProducts;
        private IOrder _order;

        private void Awake()
        {
            _customer = GetComponent<ICustomer>();
        }

        private void Update()
        {
            if (_isTakingProducts)
            {
                TakeProducts();
            }
        }
        
        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter(IOrder order)
        {
           _isTakingProducts = true;
           _order = order;

        }
        
        private void TakeProducts()
        {
            var stand = (IStand)_order.Target;
            if (stand.IsEmpty())return;
            
            var product = stand.GetAvailableProduct();
            _customer.AddProductInBasket(product);
                
            _order.OnGotProduct();
            EventStreams.Global.Publish(new OrderUpdatedEvent(_order, transform));

            if (!_order.IsCompleted) return;
            _isTakingProducts = false;
            EnterMovingToTargetState();
        }
        
        private void EnterMovingToTargetState()
        {
            _stateMachine.Enter<MovingToTargetState>();
        }
        
    }
}