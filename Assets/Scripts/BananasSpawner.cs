using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class BananasSpawner : MonoBehaviour , ISpawner
{
    public UnityEvent<Stack<GameObject>> CountBananasСhanged;
    public float _currentTime { get; set; }
    public float _delayBetweenSpawnObjects { get; set; } = 2f;
    
    [SerializeField] private GameObject _bananaPrefab;
    [SerializeField] private Transform[] _allPositionsForBananas;
    public Stack<GameObject> _allBananas = new();
    private GameObject _banana;


    private void Update()
    {
        ObjectsSpawn();
    }

    private void ObjectsSpawn()
    {
        if (_allBananas.Count>=3)
        {
            return;
        }
        if (_currentTime<_delayBetweenSpawnObjects)
        {
            _currentTime += Time.deltaTime;
        }
        if (_currentTime>=_delayBetweenSpawnObjects && _allBananas.Count<=3)
        {
            var currentParent = _allBananas.Count;
            _banana = Instantiate(_bananaPrefab, _allPositionsForBananas[currentParent]);
            _currentTime = 0f;
            _allBananas.Push(_banana);
            CountBananasСhanged?.Invoke(_allBananas);
        }
    }
}