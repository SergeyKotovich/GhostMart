using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class CornSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private GameObject _cornPrefab;
    [SerializeField] private Transform _rootProduct;
    public float _currentTime { get; set; }
    public float _delayBetweenSpawnObjects { get; set; } = 3f;
    private GameObject _corn;
    

    private void Update()
    {
        ObjectsSpawn();
    }

    private void ObjectsSpawn()
    {
        if (_currentTime<_delayBetweenSpawnObjects)
        {
            _currentTime += Time.deltaTime;
        }
        if (_currentTime>=_delayBetweenSpawnObjects && _corn == null)
        {
            _corn = Instantiate(_cornPrefab, _rootProduct );
        }
    }
    [UsedImplicitly]
    public void ResetProduct()
    {
        _corn = null;
        _currentTime = 0f;
    }

    
}