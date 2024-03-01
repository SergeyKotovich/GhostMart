using System;
using Interfaces;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    [SerializeField] private CollectingProducts _collectingProducts;
    [SerializeField] private CashRegister _cashRegister;
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
        
        if (other.gameObject.CompareTag(GlobalConstants.CASH_REGISTER))
        {
            _cashRegister.Open();
        }
        
        if (other.gameObject.CompareTag(GlobalConstants.MONEY_KEEPER))
        {
            var moneySpawner = other.GetComponent<MoneySpawner>();
            var player = (Player)_player;
            player.AddMoney(moneySpawner.GetMoney());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(GlobalConstants.CASH_REGISTER))
        {
            _cashRegister.CLose();
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