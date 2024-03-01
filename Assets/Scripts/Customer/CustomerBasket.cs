using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Customer
{
    public class CustomerBasket : IBasket
    {
        public int ProductsCount;
        private List<Product> _boughtProducts = new();
        public event Action<int> CountProductsChanged;
        public int MaxCountProduct { get; }
        public int CurrentCountProduct { get; set; }
        
        public void AddProductInBasket(Product product)
        {
            _boughtProducts.Add(product);
        }
        
        public Product GetProduct()
        {
            return null;
        }

        public Product GetSuitableProduct(TypeProduct typeProduct)
        {
            return null;
        }

        public bool IsFull()
        {
            return false;
        }
    }
}