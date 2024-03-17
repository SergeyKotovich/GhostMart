
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
        private ListItem _currentListItem;

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
           for (int i = 0; i < _customer.ShoppingList.Count; i++)
           {
               if (_customer.ShoppingList[i].StopPoint.TypeInteractablePoint == _customer.CurrentTargetType)
               {
                   _currentListItem = _customer.ShoppingList[i];
               }
           }
        }
        
        private void TakeProducts()
        {
            var stand = (IStand)_currentListItem.StopPoint;
            
            var productsOnStandCount = stand.GetProductsCount();

            if (productsOnStandCount > 0)
            {
                var product = stand.GetAvailableProduct();
                _customer.AddProductInBasket(product);
                
                _currentListItem.OnGotProduct();
               // _customer._productBarView.UpdateProductBar(shoppingList[currentPathIndex]);
            }

            if (_currentListItem.CurrentCount >= _currentListItem.MaxCount)
            {
                //_customer.Basket.ResetCurrentProductCount();
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