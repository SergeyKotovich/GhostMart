using System;


public interface IBasket
    {
        public event Action<int> CountProductsChanged;
        public int MaxCountProduct { get; }
        public int CurrentCountProduct { get; }

        public void AddProductInBasket(Product product);

        public Product GetProduct();
        public bool IsFull();

    }

