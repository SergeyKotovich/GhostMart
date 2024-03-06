using System.Collections.Generic;
using Interfaces;
using JetBrains.Annotations;
using UnityEngine;



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
        if (_allAvailableProducts.Count!=0)
        {
            ProductCounter--;
            return _allAvailableProducts.Pop();
        }

        return null;
    }

    public bool HasSpawnedProduct()
    {
        return _allAvailableProducts.Count > 0;
    }

}