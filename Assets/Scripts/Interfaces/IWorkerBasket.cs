using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IWorkerBasket : IBasket
    {
        public event Action<int> CountProductsChanged;
        public int MaxCountProduct { get; }
        public int CurrentCountProduct { get; }
        public bool IsFull();
        public bool IsEmpty();
        
        public Product GetProduct();
        public List<Product> GetAllProducts();

        public bool HasSuitableProduct(TypeProduct typeProduct);
        
        public void IncreaseMaxCountProduct();
    }
}