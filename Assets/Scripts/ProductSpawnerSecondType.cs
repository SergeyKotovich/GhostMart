using System;
using System.Collections;
using DG.Tweening;
using Interfaces;
using UnityEngine;

public class ProductSpawnerSecondType : MonoBehaviour, ISpawner
{
    [SerializeField] private ProductConfig _productConfig;
    [SerializeField] private Transform[] _allPositionsForSpawn;
    [SerializeField] private Product _productPrefab;
    [SerializeField] private StorageProductsForInteraction _storageProductsForInteraction;
    
    private IFactory _productFactory;
    private int _countSpawnedProducts;
    private float _currentTime;

    private void Awake()
    {
        _productFactory = GetComponent<IFactory>();
    }
    
    private void Update()
    {
        if (IsNotCanSpawn())
        {
            return;
        }
        _storageProductsForInteraction.DestroyProduct();
        SpawnProduct();

    }

    private bool IsNotCanSpawn()
    {
        if (_productFactory.ProductCounter >= _productConfig.MaxCountSpawnedProduct)
        {
            return true;
        }
        if (_currentTime<_productConfig._delayBetweenSpawnObjects)
        {
            _currentTime += Time.deltaTime;
            return true;
        }
        if (_countSpawnedProducts==_productConfig.MaxCountSpawnedProduct)
        {
            return true;
        }
        if (!_storageProductsForInteraction.HasProductsForInteraction())
        {
            return true;
        }

        return false;
    }

    public void SpawnProduct()
    {
        var currentIndexPoint = _countSpawnedProducts;
        var product = Instantiate(_productPrefab, _allPositionsForSpawn[currentIndexPoint]);
        product.transform.DOScale(_productConfig.ScaleProductAfterSpawn, _productConfig.SizeChangeTime).SetDelay(_productConfig._delayBetweenSpawnObjects)
            .OnComplete(() => _productFactory.OnAvailableProductsUpdated(product));
        _countSpawnedProducts = (_countSpawnedProducts+1)%_productConfig.MaxCountSpawnedProduct;
        _currentTime = default;
    }
}