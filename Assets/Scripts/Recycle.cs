using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Recycle : MonoBehaviour
{
    private List<GameObject> _listAllProductsInHands;
    private Vector3 _placeRecycling = new(-0.09f,0.9f,0.12f);
    private const int _duration = 1;

    private void Recycling(List<Product> allProducts)
    {
        foreach (var product in allProducts)
        {
            product.transform.SetParent(transform);
            product.transform.DOLocalMove(_placeRecycling, _duration);
            product.transform.DOScale(Vector3.zero, _duration)
                .OnComplete(() => Destroy(product.gameObject));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var worker = other.GetComponent<IWorker>();
        var allProducts = worker.GetAllProducts();
        if (allProducts==null)
        {
            return;
        }
        Recycling(allProducts);
        
        if (other.CompareTag(GlobalConstants.ASSISTANT_TAG))
        {
            EventStreams.Global.Publish(new ProductsAreRecycledEvent());
        }
    }
}