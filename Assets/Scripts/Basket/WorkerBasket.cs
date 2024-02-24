using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


public class WorkerBasket : MonoBehaviour, IWorkerBasket
{
    public event Action<int> CountProductsChanged;
    public int CurrentCountProduct { get; private set; }
    [field:SerializeField] public int MaxCountProduct { get; private set; }
    
    private readonly Stack<Product> _allProducts = new();
    public void AddProductInBasket(Product product)
    {
        _allProducts.Push(product);
        CountProductsChanged?.Invoke(CurrentCountProduct);
        CurrentCountProduct++;
    }

    public Product GetProduct()
    {
        if (_allProducts.Count == 0)
        {
            return null;
        }
        var product = _allProducts.Pop();
        CountProductsChanged?.Invoke(CurrentCountProduct);
        CurrentCountProduct--;
        return product;
    }

    public bool IsFull()
    {
        return CurrentCountProduct == MaxCountProduct;
    }

    public bool IsEmpty()
    {
        return _allProducts.Count == 0;
    }
}