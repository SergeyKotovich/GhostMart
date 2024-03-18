using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class ProductFactory : MonoBehaviour, IFactory
{
    public event Action CountSpawnedProductsDecreased; 
    [SerializeField] private Player.Player _player;
    private Stack<Product> _allAvailableProducts = new();
   

   public void OnAvailableProductsUpdated(Product availableProduct)
    {
        _allAvailableProducts.Push(availableProduct);
    }

    public Product GetProduct()
    {
        CountSpawnedProductsDecreased?.Invoke();
        return _allAvailableProducts.Pop();
    }

    public bool HasSpawnedProduct()
    {
        return _allAvailableProducts.Count > 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            if (_allAvailableProducts.Count <= 0 || !_player.CanPickUp)
            {
                return;
            }
            var product = GetProduct();
            _player.PickUpProduct(product);
            _player.CollectingProducts.TryToSetPosition(product);
            
        }
    }
}