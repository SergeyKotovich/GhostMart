using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StorageProductsForInteraction : MonoBehaviour
{
    [field:SerializeField] public TypeProduct TypeProduct { get; private set; }
        
    [SerializeField] private Transform[] _allPositionProductsForInteraction;
    
    private Stack<Product> _productsForInteraction = new();
    private readonly int _maxCountProductForInteraction = 4;
    private int _currentCountProductsForInteraction;
    
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
        if (_currentCountProductsForInteraction==_maxCountProductForInteraction)
        {
            return;
        }
        _productsForInteraction.Push(product);
        var indexPosition = _currentCountProductsForInteraction;
        product.transform.SetParent(_allPositionProductsForInteraction[indexPosition]);
        product.transform.DOMove(_allPositionProductsForInteraction[indexPosition].position, 1);
        product.transform.DORotate(_allPositionProductsForInteraction[indexPosition].position, 1);
        _currentCountProductsForInteraction++;
    }
    public void DestroyProduct()
    {
        var product =  _productsForInteraction.Pop();
        product.transform.DOScale(0, 0.1f).SetDelay(5).OnComplete(() => Destroy(product.gameObject));
        _currentCountProductsForInteraction--;

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