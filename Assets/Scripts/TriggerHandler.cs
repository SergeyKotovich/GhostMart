using System;
using Interfaces;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    [SerializeField] private CollectingProducts _collectingProducts;
    private IWorker _player;

    private void Awake()
    {
        _player = GetComponent<IWorker>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GlobalConstants.PRODUCT_FACTORY_TAG))
        {
            var productFactory = other.GetComponent<IFactory>();
            TakeProductFromFactory(productFactory);
        }

        if (other.gameObject.CompareTag(GlobalConstants.STAND_TAG))
        {
            var stand = other.gameObject.GetComponent<Stand>();
            HandleStandInteraction(stand);
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

    private void HandleStandInteraction(Stand stand)
    {
        if (!stand.IsAnyFreePlace() || !_player.HasProducts) return;
        
        var product = _player.GetProduct();
        if (stand.Type != product.Type)
        {
            _player.PickUpProduct(product);
            return;
        }
        stand.SetProductOnStand(product);
    }
}