using System;
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
}