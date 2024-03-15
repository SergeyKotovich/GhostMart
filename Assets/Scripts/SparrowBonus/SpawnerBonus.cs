using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnerBonus : MonoBehaviour
{
    [SerializeField] private Bonus _objectToSpawn;
    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _maxSpawnTime;
    [SerializeField] private ProductBarSpawner _productBarSpawner;

    private Bonus _currentBonus;
    private ProductBarView _productBarView;

    private void Start()
    {
        StartCoroutine(SpawnObject());
    }

    private void InitializeBonus(Bonus bonus)
    {
        _currentBonus = bonus;
        _currentBonus.BonusFlewToTarget += StartSpawning;
        _currentBonus.Initialize(_productBarView);
    }
    IEnumerator SpawnObject()
    {
        yield return new WaitForSeconds(Random.Range(1, 1));

        var spawnedObject = Instantiate(_objectToSpawn, transform.position, Quaternion.identity);
        _productBarView = _productBarSpawner.GetProductBar(spawnedObject.gameObject);

        InitializeBonus(spawnedObject);
        _currentBonus.BonusMovement.MoveToTarget(new Vector3(-4.34656954f,0f,-48.4787254f));

        var newRotation = Quaternion.Euler(0f, 180f, 0f);
        spawnedObject.transform.rotation = newRotation;
        
        spawnedObject.transform.SetParent(transform);

        yield return new WaitUntil(() => (spawnedObject.transform.position - _targetPosition).sqrMagnitude < 0.01f);
        
        _currentBonus.SwitcherStateTrigger(true);
    }

    private void StartSpawning()
    {
        StartCoroutine(SpawnObject());
        if (_productBarView != null)
        {
            _currentBonus.BonusFlewToTarget -= StartSpawning;
            Destroy(_productBarView.gameObject);
        }
    }
}