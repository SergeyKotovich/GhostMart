using System;
using Interfaces;
using UnityEngine;

public class StorageTriggerHandler : MonoBehaviour
{
    [SerializeField] private StorageProductsForInteraction _storageProductsForInteraction;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        var worker = other.GetComponent<IWorker>();
        
        var typeProduct =  _storageProductsForInteraction.TypeProduct;
        if (!worker.Basket.HasSuitableProduct(typeProduct))
        {
            return;
        }
        if (_storageProductsForInteraction.IsFoolStorage())
        {
            return;
        }

        while (!_storageProductsForInteraction.IsFoolStorage() || !worker.Basket.HasSuitableProduct(typeProduct))
        {
            var product =  worker.Basket.GetSuitableProduct(typeProduct);
            if (product==null)
            {
                return;
            }
            _storageProductsForInteraction.AddProductForInteraction(product);
        }
       

    }
}