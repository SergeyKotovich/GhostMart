using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;


public class Basket : MonoBehaviour
{
    public event Action<int> CountProductsChanged; 

    public Stack<Product> _allProducts = new();
    public int _maxCountProduct { get; private set; } = 3;
    public int _currentCountProduct { get; private set; }

    public void AddProductInBasket(Product product)
    {
        _allProducts.Push(product);
        _currentCountProduct++;
        CountProductsChanged?.Invoke(_currentCountProduct);
    }

    public Product GetProduct()
    {
        if (_allProducts.Count == 0)
        {
            return null;
        }
        var product = _allProducts.Pop();
        _currentCountProduct--;
        CountProductsChanged?.Invoke(_currentCountProduct);
        return product;
    }

    public bool IsFull()
    {
        return _currentCountProduct == _maxCountProduct;
    }

    public bool IsEmpty()
    {
        return _allProducts.Count == 0;
    }
}