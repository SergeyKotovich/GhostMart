using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BananasSpawner : MonoBehaviour , IObjectsSpawner
{
    public float _currentTime { get; set; }
    public float _delayBetweenSpawnObjects { get; set; } = 3f;
    
    [SerializeField] private GameObject _bananaPrefab;
    [SerializeField] private Transform _rootProduct;
    private List<GameObject> _allBananas = new();
    private GameObject _banana;


    private void Update()
    {
        ObjectsSpawn();
    }

    public void ObjectsSpawn()
    {
        if (_currentTime<_delayBetweenSpawnObjects)
        {
            _currentTime += Time.deltaTime;
        }
        if (_currentTime>=_delayBetweenSpawnObjects && _banana==null)
        {
            _banana = Instantiate(_bananaPrefab, _rootProduct );
           // _allBananas.Add(_banana);
        }
    }
    [UsedImplicitly]
    public void ResetProduct()
    {
        _banana = null;
        _currentTime = 0f;
    }
}