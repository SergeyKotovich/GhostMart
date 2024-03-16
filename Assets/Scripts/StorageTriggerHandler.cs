using System;
using Cysharp.Threading.Tasks;
using Interfaces;
using UnityEngine;

public class StorageTriggerHandler : MonoBehaviour
{
    [SerializeField] private TypeInteractablePoints _typeInteractablePoints;
    private IStorageable _storageProductsForInteraction;
    private int _delayBetweenAddProducts = 100;
    

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

        if (_typeInteractablePoints == TypeInteractablePoints.Stand)
        {
            AddProducts(worker, typeProduct, _delayBetweenAddProducts);
        }
        else
        {
            AddProducts(worker, typeProduct, 0);
        }
        
    }

    private async UniTask AddProducts(IWorker worker, TypeProduct typeProduct, int delayBetweenAddProducts )
    {
        while (!_storageProductsForInteraction.IsFull() || !worker.Basket.HasSuitableProduct(typeProduct))
        {
            var product = worker.Basket.GetSuitableProduct(typeProduct);
            if (product == null)
            {
                return;
            }

            _storageProductsForInteraction.AddProduct(product);
            await UniTask.Delay(delayBetweenAddProducts);
        }
    }
}