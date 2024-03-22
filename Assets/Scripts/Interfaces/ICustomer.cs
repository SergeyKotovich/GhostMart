using System;
using System.Collections.Generic;
using Customer;
using Order;
using UnityEngine;

namespace Interfaces
{
    public interface ICustomer
    {
        public List<IOrder> OrdersList { get; }
        public Vector3 PositionInLine { get; }
        
        public void SetDestination(Vector3 position);
        public bool IsAtTargetPoint();
        public void AddProductInBasket(Product product);
        public List<Product> GetBoughtProducts();

    }
}