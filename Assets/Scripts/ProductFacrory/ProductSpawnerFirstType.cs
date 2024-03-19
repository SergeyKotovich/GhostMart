using System;
using DG.Tweening;
using Interfaces;
using UnityEngine;


public class ProductSpawnerFirstType : MonoBehaviour 
{
    [SerializeField] private ProductConfig _productConfig;
    [SerializeField] private Transform[] _allPositionsForSpawn;
    [SerializeField] private Product _productPrefab;
    
    private IFactory _productFactory;
    private int _countSpawnedProducts;
    private float _currentTime;
    private int _currentIndex;

    protected virtual void Awake()
    {
        _productFactory = GetComponent<IFactory>();
        _productFactory.CountSpawnedProductsDecreased += OnCountSpawnedProductsDecreased;
    }

    protected virtual void Update()
    {
        if (CanNotSpawn())
        {
            return;
        }
        SpawnProduct();
    }

    protected virtual bool CanNotSpawn()
    {
        if (_countSpawnedProducts >= _productConfig.MaxCountSpawnedProduct)
        {
            return true;
        }

        if (_currentTime < _productConfig._delayBetweenSpawnObjects)
        {
            _currentTime += Time.deltaTime;
            return true;
        }

        return false;
    }

    protected void SpawnProduct()
    {
        var product = Instantiate(_productPrefab, _allPositionsForSpawn[_currentIndex]);
        _countSpawnedProducts++;
        product.transform.DOScale(_productConfig.ScaleProductAfterSpawn, _productConfig.SizeChangeTime).SetDelay(_productConfig._delayBetweenSpawnObjects)
            .OnComplete(() => _productFactory.OnAvailableProductsUpdated(product));
        _currentIndex = (_currentIndex+1)%_productConfig.MaxCountSpawnedProduct;
        _currentTime = default;
    }

    private void OnCountSpawnedProductsDecreased()
    {
        _countSpawnedProducts--;
    }

    private void OnDestroy()
    {
        _productFactory.CountSpawnedProductsDecreased -= OnCountSpawnedProductsDecreased;
    }
}