
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Customer
{
    public class GettingProductsState : MonoBehaviour, IState
    {
        [SerializeField] private Customer _customer;
        
        private StateMachine _stateMachine;
        private CustomerBasket _basket;
        private bool _isTakingProducts;
        
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
            _basket = new CustomerBasket();
        }

        public void OnEnter()
        {
           // _customer.GotProducts += EnterMovingToTargetState;
           _isTakingProducts = true;
        }
        
        private void TakeProducts()
        {
            var shoppingList = _customer.ShoppingList;
            var currentPathIndex = _customer.CurrentPathIndex;
            
            if (shoppingList.Count == currentPathIndex)
            {
                return;
            }
            
            var stand = (Stand)shoppingList[currentPathIndex].StopPoint;
            var productsOnStandCount = stand.GetProductsCount();

            if (productsOnStandCount > 0)
            {
                var product = stand.GetAvailableProduct();
                _basket.PutProduct(product);
                shoppingList[currentPathIndex].CurrentCount++;
                _customer._productBarView.UpdateProductBar(shoppingList[currentPathIndex]);
                _basket.ProductsCount++;
                Destroy(product);
            }

            if (_basket.ProductsCount == shoppingList[currentPathIndex].MaxCount)
            {
                _customer.CurrentPathIndex++;
                _basket.ProductsCount = 0;
                _isTakingProducts = false;
                Debug.Log("TakeProducts");
                EnterMovingToTargetState();
            }
        }
        
        private void EnterMovingToTargetState()
        {
            _stateMachine.Enter<MovingToTargetState>();
        }

        public void OnExit()
        {
            //_customer.GotProducts -= EnterMovingToTargetState;
        }
        
    }
}