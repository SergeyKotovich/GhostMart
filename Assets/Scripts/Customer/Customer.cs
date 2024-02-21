
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Customer
{
    public class Customer : MonoBehaviour
    { 
        public event Action CameToTarget;
        public event Action GotProducts;

        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;

        private CustomerBasket _basket;
        private int _currentPathIndex;
        private bool _isMoving;
        private bool _isTakingProducts;

        public List<ListItem> ShoppingList = new();

        public void Initialize(List<Stand> path)
        {
            _basket = new CustomerBasket();
            foreach (var stand in path)
            {
                var count = Random.Range(1, 3);
                var stopPoint = new ListItem(stand.PointForCustomers.position, stand, count);
                ShoppingList.Add(stopPoint);
                
                Debug.Log("stand " + stand.Type + "count " + count);
            }
        }

        void Update()
        {
            if (_navMeshAgent.remainingDistance < 1f && !_navMeshAgent.pathPending)
            {
                CameToTarget?.Invoke();
            }

            if (_isTakingProducts)
            {
                TakeProducts();
            }
        }

        public void MoveToNextPoint()
        {
            if (_currentPathIndex < ShoppingList.Count)
            {
                _navMeshAgent.SetDestination(ShoppingList[_currentPathIndex].Position);
                
                //_currentPathIndex++;
                _isMoving = true;
                _animator.SetBool("IsMoving", _isMoving);
            }
            else
            {
                _navMeshAgent.isStopped = true;
                _animator.SetBool("IsMoving", false);
            }
        }

        public void StopMoving()
        {
            _isMoving = false;
            _animator.SetBool("IsMoving", _isMoving);
        }

        public void TakeProducts()
        {
            _isTakingProducts = true;
            if (ShoppingList.Count == _currentPathIndex)
            {
                return;
            }
            var productsOnStandCount = ShoppingList[_currentPathIndex].Stand.GetProductsCount();

            if (productsOnStandCount > 0)
            {
                var product = ShoppingList[_currentPathIndex].Stand.GetAvailableProduct();
                _basket.PutProduct(product);
                _basket.ProductsCount++;
                Destroy(product);
            }

            if (_basket.ProductsCount == ShoppingList[_currentPathIndex].ProductsCount)
            {
                _currentPathIndex++;
                _basket.ProductsCount = 0;
                GotProducts?.Invoke();
                _isTakingProducts = false;
            }
        }
    }
}