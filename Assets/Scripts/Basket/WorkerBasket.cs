using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

public class WorkerBasket : MonoBehaviour, IWorkerBasket
{
    public event Action<int> CountProductsChanged;
    public int CurrentCountProduct { get; private set; }
    [field:SerializeField] public int MaxCountProduct { get; private set; }
    
    private readonly List<Product> _allProducts = new();
    public void AddProductInBasket(Product product)
    {
        //_allProducts.Push(product);
        _allProducts.Add(product);
        CountProductsChanged?.Invoke(CurrentCountProduct);
        CurrentCountProduct++;
    }

    public Product GetProduct()
    {
        if (_allProducts.Count == 0)
        {
            return null;
        }
        
        var product = _allProducts.Last();
        _allProducts.RemoveAt(_allProducts.Count-1);
        CountProductsChanged?.Invoke(CurrentCountProduct);
        CurrentCountProduct--;
        return product;
        
        // var product = _allProducts.Pop();
    }
    public Product GetSuitableProduct(TypeProduct typeProduct)
    {
        if (_allProducts.Count == 0)
        {
            return null;
        }
        for (var i = 0; i < _allProducts.Count; i++)
        {
            if (_allProducts[i].Type == typeProduct)
            {
                var product = _allProducts[i];
                _allProducts.RemoveAt(i);
                CountProductsChanged?.Invoke(CurrentCountProduct);
                CurrentCountProduct--;
                return product;
            }
        }
       
        // var product = _allProducts.Pop();
        return null;
    }

    public bool IsFull()
    {
        return CurrentCountProduct == MaxCountProduct;
    }

    public bool IsEmpty()
    {
        return _allProducts.Count == 0;
    }

    public bool HasSuitableProduct(TypeProduct typeProduct)
    {
        return _allProducts.Any(product => product.Type == typeProduct);
    }
}