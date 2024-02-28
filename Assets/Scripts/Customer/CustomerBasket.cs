using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Customer
{
    public class CustomerBasket : IBasket
    {
        public int ProductsCount { get; private set; }
        private List<Product> _boughtProducts = new();

        public void AddProductInBasket(Product product)
        {
            _boughtProducts.Add(product);
            ProductsCount++;
        }
        
        public Product GetProduct()
        {
            return null;
        }
        
        public void ResetCurrentProductCount()
        {
            ProductsCount = 0;
        }

        public int GetTotalProductPrice()
        {
            var amount = 0;
            
            foreach (var boughtProduct in _boughtProducts)
            {
                amount += boughtProduct.Price;
            }

            return amount;
        }
    }
}