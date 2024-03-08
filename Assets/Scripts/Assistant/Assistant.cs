using System;
using System.Collections.Generic;
using Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Assistant : MonoBehaviour, IWorker
{
    [field:SerializeField]
    public NavMeshAgent NavMeshAgent { get; private set; }
    [field:SerializeField]
    public WorkerTypes Type { get; private set; }
    public AbilitiesController AbilitiesController { get; private set; }
    public IWorkerBasket Basket { get; private set; }

    private void Awake()
    {
        Basket = GetComponent<IWorkerBasket>();
        AbilitiesController = new AbilitiesController(Basket, this);
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

    public void IncreaseSpeed()
    {
        NavMeshAgent.speed++;
    }

}