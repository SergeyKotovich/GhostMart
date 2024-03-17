using System;
using System.Collections.Generic;


public interface IBasket
    {
        public void AddProduct(Product product);
        public Product GetSuitableProduct(TypeProduct typeProduct);
    }

