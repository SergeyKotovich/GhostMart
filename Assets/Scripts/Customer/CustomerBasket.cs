using System.Collections.Generic;
using UnityEngine;

namespace Customer
{
    public class CustomerBasket
    {
        public int ProductsCount;
        private List<Product> _boughtProducts = new();

        public void PutProduct(Product product)
        {
            _boughtProducts.Add(product);
            Debug.Log(product.tag+" В корзине");
        }
    }
}