using System;
using Interfaces;
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
}