using System;
using System.Collections.Generic;
using Customer;
using UnityEngine;

namespace Interfaces
{
    public interface ICustomer
    {
        public List<ListItem> ShoppingList { get; }
        public Vector3 PositionInLine { get; }

        public TypeInteractablePoints CurrentTargetType { get; }

        public void SetDestination(Vector3 position);
        public bool IsAtTargetPoint();
        public void AddProductInBasket(Product product);
        public List<Product> GetBoughtProducts();
        public void SetCurrentTargetType(TypeInteractablePoints type);

    }
}