
using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Customer
{
    public class GettingProductsState : MonoBehaviour, IState
    {
        private ICustomer _customer;
        
        private StateMachine _stateMachine;
        private CustomerBasket _basket;
        private bool _isTakingProducts;

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
            _basket = new CustomerBasket();
        }

        public void OnEnter()
        {
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
                _basket.AddProductInBasket(product);
                shoppingList[currentPathIndex].CurrentCount++;
                _customer._productBarView.UpdateProductBar(shoppingList[currentPathIndex]);
                _basket.ProductsCount++;
                //_customer.Basket.AddProductInBasket(product);
                Destroy(product.gameObject);
            }

            if (_basket.ProductsCount == shoppingList[currentPathIndex].MaxCount)
            {
                _customer.CurrentPathIndex++;
                _basket.ProductsCount = 0;
                _isTakingProducts = false;

                EnterMovingToTargetState();
            }
        }
        
        private void EnterMovingToTargetState()
        {
            _stateMachine.Enter<MovingToTargetState>();
        }
        
    }
}