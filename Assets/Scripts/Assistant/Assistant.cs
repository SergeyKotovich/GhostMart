using System;
using System.Collections.Generic;
using Interfaces;
using Unity.VisualScripting;
using UnityEngine;

public class Assistant : MonoBehaviour, IWorker
{
    public IWorkerBasket Basket { get; private set; } 

    private void Awake()
    {
        Basket = GetComponent<IWorkerBasket>();
    }

    public void PickUpProduct(Product product)
    {
        Basket.AddProductInBasket(product);
    }

    public Product GetProduct()
    {
        return Basket.GetProduct();
    }

    public List<Product> GetAllProducts()
    {
        List<Product> allProducts = new List<Product>();
        var productsCount = Basket.CurrentCountProduct;
        for (int i = 0; i < productsCount; i++)
        {
            allProducts.Add(Basket.GetProduct());
        }

        return allProducts;
    }
}