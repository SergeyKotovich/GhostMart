using System.Collections.Generic;

namespace Interfaces
{
    public interface IFactory
    {
        public int ProductCounter { get; }
        
        
        public void OnAvailableProductsUpdated(Product availableProduct);
        public Product GetProduct();
        public bool HasSpawnedProduct();
    }
}