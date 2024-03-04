using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Recycle : MonoBehaviour
{
    private List<GameObject> _listAllProductsInHands;

    private void Recycling(List<Product> allProducts)
    {
        foreach (var product in allProducts)
        {
            product.transform.SetParent(transform);
            product.transform.DOLocalMove(new Vector3(-0.0890000015f,0.921000004f,0.116999999f), 1f);
            product.transform.DOScale(new Vector3(0, 0, 0), 1f).OnComplete(() =>Destroy(product,1f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            var worker = other.GetComponent<IWorker>();
            var allProducts = worker.GetAllProducts();
            Recycling(allProducts);
        }

        if (other.CompareTag(GlobalConstants.ASSISTANT_TAG))
        {
            var worker = other.GetComponent<IWorker>();
            var allProducts = worker.GetAllProducts();
            Recycling(allProducts);
            EventStreams.Global.Publish(new ProductsAreRecycledEvent());
        }
    }
}