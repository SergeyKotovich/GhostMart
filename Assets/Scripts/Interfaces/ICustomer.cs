using System;
using System.Collections.Generic;
using Customer;
using UnityEngine;

namespace Interfaces
{
    public interface ICustomer
    {
        public MovementController.MovementController MovementController { get; }
        public int CurrentPathIndex { get; set; }
        public ProductBarView _productBarView { get; }
        public List<ListItem> ShoppingList { get; }
        public ICustomerBasket Basket { get; }
        public Vector3 PositionInLine { get; }
        public int ProductsCountInBasket { get; }

        public void SetPositionInLine(Vector3 position);

    }
}