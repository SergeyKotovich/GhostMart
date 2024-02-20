using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ProductSpawner : MonoBehaviour
{
    public UnityEvent<Stack<GameObject>> CountProductСhanged;
    
    [SerializeField] private float _delayBetweenSpawnObjects ;
    [SerializeField] private SpawnerConfig _spawnerConfig;
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
            var product = Instantiate(_spawnerConfig.Prefab, _allPositionsForProduct[currentParent]);
            product.transform.DOScale(_spawnerConfig.ScaleProductAfterSpawn, _spawnerConfig.SizeChangeTime).OnComplete (() => _allSpawnedProduct.Push(product));
            
            _currentTime = default;
            
            CountProductСhanged?.Invoke(_allSpawnedProduct);
        }
    }
}