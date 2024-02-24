
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Customer
{
    public class Customer : MonoBehaviour, ICollectable, ICustomer
    {
        public int CurrentPathIndex { get; set; }
        public WorkerBasket WorkerBasket { get; }
        private bool _isMoving;
        
        public ProductBarView _productBarView { get; private set; }
        public List<ListItem> ShoppingList { get; } = new();
        

        public void Initialize(List<Stand> path, ProductBarView productBarView)
        {
            foreach (var stand in path)
            {
                var count = Random.Range(1, 5);
                var stopPoint = new ListItem(stand.PointForCustomers.position, stand, count);
                ShoppingList.Add(stopPoint);
            }

            _productBarView = productBarView;
        }
    }
}