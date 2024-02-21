using System.Collections.Generic;
using UnityEngine;

public class ProductFactory : MonoBehaviour
{
    private Queue<GameObject> _allAvailableProducts = new();

    public int ProductCounter => _allAvailableProducts.Count;

    public void AddProduct(GameObject product)
    {
        _allAvailableProducts.Enqueue(product);
    }

    public GameObject GetProduct()
    {
        return _allAvailableProducts.Count > 0 ? _allAvailableProducts.Dequeue() : null;
    }
}