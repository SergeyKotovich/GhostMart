using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CornSpawner : MonoBehaviour, ISpawner
{
    public UnityEvent<Stack<GameObject>> CountCornСhanged;
    public float _currentTime { get; set; }
    public float _delayBetweenSpawnObjects { get; set; } = 2f;
    
    [SerializeField] private GameObject _cornPrefab;
    [SerializeField] private Transform[] _allPositionsForCorn;
    private Stack<GameObject> _allCorn = new();
    
    
    private void Update()
    {
        ObjectsSpawn();
    }

    private void ObjectsSpawn()
    {
        if (_allCorn.Count>=3)
        {
            return;
        }
        if (_currentTime<_delayBetweenSpawnObjects)
        {
            _currentTime += Time.deltaTime;
        }
        if (_currentTime>=_delayBetweenSpawnObjects && _allCorn.Count<=3)
        {
            var currentParent = _allCorn.Count;
            var corn = Instantiate(_cornPrefab, _allPositionsForCorn[currentParent]);
            corn.transform.DOScale(new Vector3(0.33f, 0.3f, 0.5f), 0.5f).OnComplete (() => _allCorn.Push(corn));
            _currentTime = 0f;
            
            CountCornСhanged?.Invoke(_allCorn);
        }
    }
}