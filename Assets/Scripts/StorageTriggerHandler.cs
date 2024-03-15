using System;
using Interfaces;
using UnityEngine;

public class StorageTriggerHandler : MonoBehaviour
{
    private IStorageable _storageProductsForInteraction;

    private void Awake()
    {
        _storageProductsForInteraction = GetComponent<IStorageable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalConstants.PLAYER_TAG)|| other.CompareTag(GlobalConstants.ASSISTANT_TAG))
        {
            var worker = other.GetComponent<IWorker>();
        
            var typeProduct =  _storageProductsForInteraction.TypeProduct;
            if (!worker.Basket.HasSuitableProduct(typeProduct))
            {
                return;
            }
            if (_storageProductsForInteraction.IsFull())
            {
                return;
            }

            if (!worker.Basket.HasSuitableProduct(typeProduct))
            {
                EventStreams.Global.Publish(new BasketIsEmpty());
            }

            while (!_storageProductsForInteraction.IsFull() || !worker.Basket.HasSuitableProduct(typeProduct))
            {
                var product =  worker.Basket.GetSuitableProduct(typeProduct);
                if (product==null)
                {
                    return;
                }
                _storageProductsForInteraction.AddProduct(product);
                if (_storageProductsForInteraction.IsFull())
                {
                    EventStreams.Global.Publish(new StorageIsFull());
                }
            }
        }
        
       

    }
}