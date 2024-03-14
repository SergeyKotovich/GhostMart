using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Customer
{
    public class CustomerBasket : ICustomerBasket
    {
        public int ProductsCount { get; private set; }
        public List<Product> BoughtProducts { get; } = new();

        public void AddProductInBasket(Product product)
        {
            BoughtProducts.Add(product);
            ProductsCount++;
        }
        

        public int GetProductsCount()
        {
            return BoughtProducts.Count;
        }

        public void ResetCurrentProductCount()
        {
            ProductsCount = 0;
        }

        //TODO: remove
        public Product GetSuitableProduct(TypeProduct typeProduct)
        {
            return null;
        }
        
        public int GetTotalProductPrice()
        {
            var amount = 0;
            
            foreach (var boughtProduct in BoughtProducts)
            {
                amount += boughtProduct.Price;
            }

            return amount;
        }
    }
}