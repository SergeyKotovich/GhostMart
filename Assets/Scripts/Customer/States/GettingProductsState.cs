
using System;
using System.Collections.Generic;
using DG.Tweening;
using Interfaces;
using UnityEngine;

namespace Customer
{
    public class GettingProductsState : MonoBehaviour, IState
    {
        private ICustomer _customer;
        private StateMachine _stateMachine;
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
            
            var stand = (IStand)shoppingList[currentPathIndex].StopPoint;
            var productsOnStandCount = stand.GetProductsCount();

            if (productsOnStandCount > 0)
            {
                var product = stand.GetAvailableProduct();
                _customer.Basket.AddProductInBasket(product);
                
                shoppingList[currentPathIndex].CurrentCount++;
                _customer._productBarView.UpdateProductBar(shoppingList[currentPathIndex]);
                product.transform.DOScale(Vector3.zero, 0.2f);
            }

            if (_customer.ProductsCountInBasket >= shoppingList[currentPathIndex].MaxCount)
            {
                _customer.CurrentPathIndex++;
                _customer.Basket.ResetCurrentProductCount();
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