using System;
using System.Collections.Generic;
using DG.Tweening;
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
    private Stack<GameObject> _allBananas = new();
    
    
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
            var banana = Instantiate(_bananaPrefab, _allPositionsForBananas[currentParent]);
            banana.transform.DOScale(new Vector3(20, 4, 4), 0.5f);
            _currentTime = 0f;
            _allBananas.Push(banana);
            CountBananasСhanged?.Invoke(_allBananas);
        }
    }
}