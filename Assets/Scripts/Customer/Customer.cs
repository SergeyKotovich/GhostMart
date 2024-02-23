
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
    public class Customer : MonoBehaviour
    {
        public int CurrentPathIndex;
        private bool _isMoving;
        
        public ProductBarView _productBarView;

        public List<ListItem> ShoppingList { get; private set; } = new();

        public void Initialize(List<Stand> path, ProductBarView productBarView)
        {
            foreach (var stand in path)
            {
                var count = Random.Range(1, 3);
                var stopPoint = new ListItem(stand.PointForCustomers.position, stand, count);
                ShoppingList.Add(stopPoint);
            }

            _productBarView = productBarView;
        }
        
    }
}