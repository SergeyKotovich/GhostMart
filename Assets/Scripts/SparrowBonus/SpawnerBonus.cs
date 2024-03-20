using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using SparrowBonus;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerBonus : MonoBehaviour
{
    [SerializeField] private Bonus _bonusPrefab;
    [SerializeField] private int _minSpawnTime;
    [SerializeField] private int _maxSpawnTime;
    [SerializeField] private OrderViewSpawner orderViewSpawner;
    [SerializeField] private SparrowLandingPoint _sparrowLandingPoint;

    private Bonus _currentBonus;

    private void Start()
    {
        Spawn();
    }
    private async UniTask Spawn()
    {
        await UniTask.Delay(Random.Range(_minSpawnTime, _maxSpawnTime));

        _currentBonus = Instantiate(_bonusPrefab, transform);
        InitializeBonus(_currentBonus);
        
        var targetPosition = _sparrowLandingPoint.PointForCustomers.position;
        _currentBonus.BonusMovement.MoveToTarget(targetPosition, OnMovementComplete);
    }

    private void OnMovementComplete()
    {
        orderViewSpawner.Spawn(_currentBonus.transform);
        _currentBonus.SetTriggerState(true);
        _currentBonus.OnOrderUpdated();
    }

    private void InitializeBonus(Bonus bonus)
    {
        _currentBonus = bonus;
        _currentBonus.Initialize(_sparrowLandingPoint);
        _currentBonus.BonusFlewToExit += StartSpawning;
    }

    private void StartSpawning()
    {
        Spawn();
    }

    private void OnDestroy()
    {
        _currentBonus.BonusFlewToExit -= StartSpawning;
    }
}