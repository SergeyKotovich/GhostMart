using System;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IBasket
    {
        public event Action<int> CountProductsChanged;
        public int MaxCountProduct { get; }
        public int CurrentCountProduct { get; }

        public void AddProductInBasket(Product product);

        public Product GetProduct();
        
    }
}