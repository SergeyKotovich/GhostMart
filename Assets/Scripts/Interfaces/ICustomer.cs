using System;
using System.Collections.Generic;
using Customer;
using UnityEngine;

namespace Interfaces
{
    public interface ICustomer
    {
        public Customer.MovementController MovementController { get; }
        public int CurrentPathIndex { get; set; }
        public ProductBarView _productBarView { get; }
        public List<ListItem> ShoppingList { get; }
        public IBasket Basket { get; }
        public Vector3 PositionInLine { get; }
        
        public void SetDestination(Vector3 destination);

        public void StopMoving();

        public bool IsAtTargetPoint();

    }
}