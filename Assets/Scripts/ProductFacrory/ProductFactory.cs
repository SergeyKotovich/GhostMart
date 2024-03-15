using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class ProductFactory : MonoBehaviour, IFactory
{
    [SerializeField] private Player.Player _player;
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