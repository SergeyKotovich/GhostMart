using System.Collections.Generic;
using Interfaces;
using JetBrains.Annotations;
using UnityEngine;

//[RequireComponent(typeof(ProductSpawner))]

public class ProductFactory : MonoBehaviour, IFactory
{
   private Stack<Product> _allAvailableProducts = new();
   public int ProductCounter { get; private set; }

   public void OnAvailableProductsUpdated(Product availableProduct)
    {
        _allAvailableProducts.Push(availableProduct);
        ProductCounter++;
    }

    public Product GetProduct()
    {
        ProductCounter--;
        return _allAvailableProducts.Pop();
    }

    public bool HasSpawnedProduct()
    {
        return _allAvailableProducts.Count > 0;
    }

}