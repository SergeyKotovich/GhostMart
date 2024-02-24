using System;
using Interfaces;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    [SerializeField] private CollectingProducts _collectingProducts;
    private ICollectable _player;

    private void Awake()
    {
        _player = GetComponent<ICollectable>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GlobalConstants.PRODUCT_FACTORY_TAG))
        {
            var productFactory = other.GetComponent<IFactory>();
            TakeProductFromFactory(productFactory);
        }
        
        if (other.gameObject.CompareTag(GlobalConstants.STAND))
        {
            var stand = other.gameObject.GetComponent<Stand>();
            if (!stand.IsAnyFreePlace())
            {
                return;
            }
            
            var product = _player.WorkerBasket.GetProduct();
            
            if (product == null) return;
            stand.SetProductOnStand(product);

        }
              
    }

    private void TakeProductFromFactory(IFactory productFactory)
    {
        if (!_player.CanPickUp || !productFactory.HasSpawnedProduct())
        {
            return;
        }
        var product = productFactory.GetProduct();
        _player.PickUpProduct(product);
        _collectingProducts.SetPosition(product);
        
    }
}