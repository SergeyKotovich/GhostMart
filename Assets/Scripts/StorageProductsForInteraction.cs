using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Interfaces;
using UnityEngine;

public class StorageProductsForInteraction : MonoBehaviour, IStorageable
{
    [field:SerializeField] public TypeProduct TypeProduct { get; private set; }
        
    [SerializeField] private Transform[] _allPositionProductsForInteraction;
    
    private Stack<Product> _productsForInteraction = new();
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
            _productsForInteraction.Push(product);
            product.transform.SetParent(_allPositionProductsForInteraction[_currentCountProductsForInteraction]);
            product.transform.DOMove(_allPositionProductsForInteraction[_currentCountProductsForInteraction].position, 1);
            product.transform.DORotate(_allPositionProductsForInteraction[_currentCountProductsForInteraction].position, 1);
            _currentCountProductsForInteraction++;
        }
       
    }
    public async UniTask DestroyProduct()
    {
        await UniTask.Delay(5000);
        var product =  _productsForInteraction.Pop();
        product.transform.DOScale(0, 0.1f).OnComplete(() => Destroy(product.gameObject));
        _currentCountProductsForInteraction--;
    }
    
    public bool IsFull()
    {
        if (_currentCountProductsForInteraction == _maxCountProductForInteraction)
        {
            return true;
        }

        return false;
    }
}