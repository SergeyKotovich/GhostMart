using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnerBonus : MonoBehaviour
{
    [SerializeField] private Bonus _objectToSpawn;
    [SerializeField] private Vector3 _targetPosition; // Vector3(-4.34656954,0,-48.4787254)
    [SerializeField] private float _minSpawnTime = 30f;
    [SerializeField] private float _maxSpawnTime = 120f;
    [SerializeField] private float _speed;
    [SerializeField] private ProductBarSpawner _productBarSpawner;

    private Bonus _currentBonus;
    private ProductBarView _productBar;

    private void Start()
    {
        StartCoroutine(SpawnObject());
    }

    private void InitializeBonus(Bonus bonus)
    {
        _currentBonus = bonus;
        _currentBonus.BonusFlewToTarget += StartSpawning;
    }
    IEnumerator SpawnObject()
    {
        yield return new WaitForSeconds(Random.Range(_minSpawnTime, _maxSpawnTime));

        var spawnedObject = Instantiate(_objectToSpawn, transform.position, Quaternion.identity);
        InitializeBonus(spawnedObject);
        _currentBonus.BonusMovement.MoveToTarget(new Vector3(-4.34656954f,0f,-48.4787254f));

        var newRotation = Quaternion.Euler(0f, 180f, 0f);
        spawnedObject.transform.rotation = newRotation;
        
        spawnedObject.transform.SetParent(transform);

        // Ожидание, пока объект достигнет позиции
        yield return new WaitUntil(() => (spawnedObject.transform.position - _targetPosition).sqrMagnitude < 0.01f);
        
        _currentBonus.SwitcherStateTrigger(true);
        
        _productBar = _productBarSpawner.GetProductBar(spawnedObject.gameObject);

    }

    private void StartSpawning()
    {
        StartCoroutine(SpawnObject());
        if (_productBar != null)
        {
            _currentBonus.BonusFlewToTarget -= StartSpawning;
            Destroy(_productBar.gameObject);
        }
    }
}