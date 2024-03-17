using System;
using System.Collections.Generic;
using DG.Tweening;
using Interfaces;
using UnityEngine;

namespace Customer
{
    public class CustomerBasket : ICustomerBasket
    {
        public int ProductsCount { get; private set; }
        public List<Product> BoughtProducts { get; } = new();

        public void AddProduct(Product product)
        {
            BoughtProducts.Add(product);
            product.transform.DOScale(Vector3.zero, 0.2f);
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
    }
}