using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ProductSpawner : MonoBehaviour
{
    public UnityEvent<Stack<GameObject>> CountProductСhanged;
    
    [SerializeField] private float _delayBetweenSpawnObjects ;
    [FormerlySerializedAs("_spawnerConfig")] [SerializeField] private ProductConfig productConfig;
    [SerializeField] private Transform[] _allPositionsForProduct;
    
    private Stack<GameObject> _allSpawnedProduct = new();
    private int _maxCountSpawnedProduct = 3;
    private float _currentTime;
    
    
    private void Update()
    {
        ProductSpawn();
    }

    private void ProductSpawn()
    {
        if (_allSpawnedProduct.Count>=_maxCountSpawnedProduct)
        {
            return;
        }
        if (_currentTime<_delayBetweenSpawnObjects)
        {
            _currentTime += Time.deltaTime;
        }
        if (_currentTime>=_delayBetweenSpawnObjects && _allSpawnedProduct.Count<=_maxCountSpawnedProduct)
        {
            var currentParent = _allSpawnedProduct.Count;
            var product = Instantiate(productConfig.Prefab, _allPositionsForProduct[currentParent]);
            product.transform.DOScale(productConfig.ScaleProductAfterSpawn, productConfig.SizeChangeTime).OnComplete (() => _allSpawnedProduct.Push(product));
            
            _currentTime = default;
            
            CountProductСhanged?.Invoke(_allSpawnedProduct);
        }
    }
}