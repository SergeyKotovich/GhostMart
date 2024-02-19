using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ProductSpawner : MonoBehaviour
{
    [SerializeField] private float _delayBetweenSpawnObjects ;
    [SerializeField] private ProductConfig productConfig;
    [SerializeField] private Transform[] _allPositionsForProduct;
    [SerializeField] private ProductFactory _productFactory;
    
    private Stack<GameObject> _allSpawnedProduct = new();
    private int _maxCountSpawnedProduct = 3;
    private float _currentTime;


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
        if (_currentTime>=_delayBetweenSpawnObjects && _allSpawnedProduct.Count<=_maxCountSpawnedProduct)
        {
            var currentIndexPoint = _productFactory.ProductCounter;
            var product = Instantiate(productConfig.Prefab, _allPositionsForProduct[currentIndexPoint]);
            product.transform.DOScale(productConfig.ScaleProductAfterSpawn, productConfig.SizeChangeTime).
                OnComplete (() => _productFactory.OnAvailableProductsUpdated(product));
            //_productCounter--;
            
            _currentTime = default;
        }
    }
}