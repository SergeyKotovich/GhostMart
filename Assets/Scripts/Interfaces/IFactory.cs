using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IFactory
    {
        public event Action CountSpawnedProductsDecreased; 
        public void OnAvailableProductsUpdated(Product availableProduct);
        public Product GetProduct();
        public bool HasSpawnedProduct();
    }
}