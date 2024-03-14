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
        public MovementController.MovementController MovementController { get; private set; }
        public int CurrentPathIndex { get; set; }
        public ICustomerBasket Basket { get; private set; }
        public int ProductsCountInBasket => Basket.ProductsCount;
        public ProductBarView _productBarView { get; private set; }
        public List<ListItem> ShoppingList { get; } = new();
        public Vector3 PositionInLine { get; private set; }


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

        public void SetPositionInLine(Vector3 position)
        {
            PositionInLine = position;
        }
        
    }
}