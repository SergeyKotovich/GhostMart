using System;
using System.Collections.Generic;


public interface IBasket
    {
        public void AddProductInBasket(Product product);
        public Product GetProduct();
        public Product GetSuitableProduct(TypeProduct typeProduct);
    }

