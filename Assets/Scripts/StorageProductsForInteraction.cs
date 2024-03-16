using System;
using System.Collections.Generic;
using DG.Tweening;
using Interfaces;
using UnityEngine;

public class StorageProductsForInteraction : MonoBehaviour, IStorageable
{
    [field:SerializeField] public TypeProduct TypeProduct { get; private set; }
        
    [SerializeField] private Transform[] _allPositionProductsForInteraction;
    
    private Queue<Product> _productsForInteraction = new();
    private readonly int _maxCountProductForInteraction = 4;
    private int _currentCountProductsForInteraction;
    private int _indexPosition;

    public bool HasProductsForInteraction()
    {
        if (_productsForInteraction.Count!=0)
        {
            return true;
        }

        return false;
    }
    public void AddProduct(Product product)
    {
        if (_currentCountProductsForInteraction!=_maxCountProductForInteraction)
        {
            _productsForInteraction.Enqueue(product);
            product.transform.SetParent(_allPositionProductsForInteraction[_indexPosition]);
            product.transform.DOMove(_allPositionProductsForInteraction[_indexPosition].position, 1);
            product.transform.DORotate(_allPositionProductsForInteraction[_indexPosition].position, 1);
            _indexPosition = (_indexPosition + 1) % _maxCountProductForInteraction;
            _currentCountProductsForInteraction++;
        }
       
    }
    public void DestroyProduct()
    {
        var product =  _productsForInteraction.Dequeue();
        product.transform.DOScale(0, 0.1f)
            .SetDelay(5).OnComplete(() => Destroy(product.gameObject))
            .OnComplete(LessenCurrentCountProductsForInteraction);
    }

    private void LessenCurrentCountProductsForInteraction()
    {
        _currentCountProductsForInteraction--;
    }

    public bool IsFull()
    {
        if (_productsForInteraction.Count == _maxCountProductForInteraction)
        {
            return true;
        }

        return false;
    }
}