using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Customer
{
    public class Customer : MonoBehaviour,ICollectable, ICustomer
    {
        public int CurrentPathIndex { get; set; }
        public IBasket WorkerBasket { get; }

        public void PickUpProduct(Product product)
        {
            
        }

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