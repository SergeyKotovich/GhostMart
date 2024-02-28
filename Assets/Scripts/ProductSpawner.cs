using System;
using DG.Tweening;
using Interfaces;
using UnityEngine;

[RequireComponent(typeof(ProductFactory))]
public class ProductSpawner : MonoBehaviour
{
    [SerializeField] private TypeProduct _typeProduct;
    [SerializeField] private StorageProductsForInteraction _storageProductsForInteraction;
    [SerializeField] private ProductConfig productConfig;
    [SerializeField] private Transform[] _allPositionsForSpawn;
    [SerializeField] private Product _productPrefab;
    [SerializeField] private int _maxCountSpawnedProduct  = 3 ; 
    [SerializeField] private float _delayBetweenSpawnObjects = 2 ;
    
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

    protected virtual void ProductSpawn()
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
            var product = Instantiate(_productPrefab, _allPositionsForSpawn[currentIndexPoint]);
            
            product.transform.DOScale(productConfig.ScaleProductAfterSpawn, productConfig.SizeChangeTime).
                OnComplete (() => _productFactory.OnAvailableProductsUpdated(product));
            
            _currentTime = default;
        }
    }
}