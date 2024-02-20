using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ProductFactory : MonoBehaviour
{
   public Stack<GameObject> _allAvailableProducts = new();
   public int ProductCounter { get; private set; }

   public void OnAvailableProductsUpdated(GameObject availableProduct)
    {
        _allAvailableProducts.Push(availableProduct);
        ProductCounter++;
    }

    public GameObject GetProduct()
    {
        if (_allAvailableProducts.Count!=0)
        {
            ProductCounter--;
            return _allAvailableProducts.Pop();
        }

        return null;
    }

}