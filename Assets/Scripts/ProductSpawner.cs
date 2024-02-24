using System;
using DG.Tweening;
using Interfaces;
using UnityEngine;

[RequireComponent(typeof(ProductFactory))]
public class ProductSpawner : MonoBehaviour
{
    [SerializeField] private float _delayBetweenSpawnObjects ;
    [SerializeField] private ProductConfig productConfig;
    [SerializeField] private Transform[] _allPositionsForProduct;
    [SerializeField] private Product _productPrefab;
    
    private readonly int _maxCountSpawnedProduct = 3;
    private float _currentTime;
    private IFactory _productFactory;

    private void Awake()
    {
        _productFactory = GetComponent<IFactory>();
    }

    private void Update()
    {
        ProductSpawn();
    }

    private void ProductSpawn()
    {
        if (_productFactory.ProductCounter>=_maxCountSpawnedProduct)
        {
            return;
        }
        if (_currentTime<_delayBetweenSpawnObjects)
        {
            _currentTime += Time.deltaTime;
        }
        if (_currentTime>=_delayBetweenSpawnObjects)
        {
            var currentIndexPoint = _productFactory.ProductCounter;
            var product = Instantiate(_productPrefab, _allPositionsForProduct[currentIndexPoint]);
            
            product.transform.DOScale(productConfig.ScaleProductAfterSpawn, productConfig.SizeChangeTime).
                OnComplete (() => _productFactory.OnAvailableProductsUpdated(product));
            
            _currentTime = default;
        }
    }
}