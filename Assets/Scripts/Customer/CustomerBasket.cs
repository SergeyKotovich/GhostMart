using System.Collections.Generic;
using UnityEngine;

namespace Customer
{
    public class CustomerBasket
    {
        public int ProductsCount;
        private List<GameObject> _boughtProducts = new();

        public void PutProduct(GameObject product)
        {
            _boughtProducts.Add(product);
            Debug.Log(product.tag+" В корзине");
        }
    }
}