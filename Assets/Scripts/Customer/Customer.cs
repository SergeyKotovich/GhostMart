using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Customer
{
    public class Customer : MonoBehaviour, ICustomer
    {
        [field:SerializeField]
        public MovementController MovementController { get; private set; }
        public int CurrentPathIndex { get; set; }
        public IBasket Basket { get; private set; }
        private bool _isMoving;
        
        public ProductBarView _productBarView { get; private set; }
        public List<ListItem> ShoppingList { get; } = new();
        public Vector3 PositionInLine { get; private set; }
        [SerializeField]
        private NavMeshAgent _navMeshAgent;

        public void Initialize(List<IInteractable> path, ProductBarView productBarView)
        {
            foreach (var stand in path)
            {
                var count = Random.Range(1, 5);
                var stopPoint = new ListItem(stand.PointForCustomers.position, stand, count);
                ShoppingList.Add(stopPoint);
            }

            _productBarView = productBarView;
            Basket = new CustomerBasket();
        }

        public void SetDestination(Vector3 destination)
        {
            MovementController.SetDestination(destination);
            PositionInLine = destination;
        }
        
        private void Update()
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance && !_navMeshAgent.pathPending)
            {
                StopMoving();
            }
        }

        public void StopMoving()
        {
            MovementController.StopMoving();
        }
        
        public bool IsAtTargetPoint()
        {
            return MovementController.IsAtTargetPoint();
        }
    }
}