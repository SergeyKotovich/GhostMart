using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StorageProductsForInteraction : MonoBehaviour
{
    [SerializeField] private Transform[] _allPositionProductsForInteraction;
    private Stack<Product> _productsForInteraction = new();
    private readonly int _maxCountProductForInteraction = 4;
    

    public bool HasProductsForInteraction()
    {
        if (_productsForInteraction.Count!=0)
        {
            return true;
        }

        return false;
    }
    public void AddProductForInteraction(Product product)
    {
        if (_productsForInteraction.Count==_maxCountProductForInteraction)
        {
            return;
        }
        SetProductPosition(product);
    }

    private void SetProductPosition(Product product)
    {
        var indexPosition = _productsForInteraction.Count;
        product.transform.SetParent(_allPositionProductsForInteraction[indexPosition]);
        product.transform.DOMove(_allPositionProductsForInteraction[indexPosition].position, 1).OnComplete(() => _productsForInteraction.Push(product));
    }

    public void TakeProduct()
    {
       var product =  _productsForInteraction.Pop();
       product.transform.DOScale(0, 1).OnComplete(() => Destroy(product.gameObject));
    }

    public bool IsFoolStorage()
    {
        if (_productsForInteraction.Count == _maxCountProductForInteraction)
        {
            return true;
        }

        return false;
    }
}