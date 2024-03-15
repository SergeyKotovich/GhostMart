using System;
using Cysharp.Threading.Tasks;
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
        if (!other.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            return;
        }
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

        AddProducts(worker, typeProduct);
       

    }

    private async UniTask AddProducts(IWorker worker, TypeProduct typeProduct)
    {
        while (!_storageProductsForInteraction.IsFull() || !worker.Basket.HasSuitableProduct(typeProduct))
        {
            var product = worker.Basket.GetSuitableProduct(typeProduct);
            if (product == null)
            {
                return;
            }
            _storageProductsForInteraction.AddProduct(product);
            await UniTask.Delay(100);
        }
    }
}