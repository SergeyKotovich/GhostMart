using System;
using System.Collections;
using SparrowBonus;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnerBonus : MonoBehaviour
{
    [SerializeField] private Bonus _bonusPrefab;
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _maxSpawnTime;
    [SerializeField] private OrderViewSpawner orderViewSpawner;
    [SerializeField] private SparrowLandingPoint _sparrowLandingPoint;

    private Bonus _currentBonus;
    private OrderView _orderView;

    private void Start()
    {
        StartSpawning();
    }
    private IEnumerator SpawnObject()
    {
        yield return new WaitForSeconds(Random.Range(_minSpawnTime, _maxSpawnTime));

        var bonus = Instantiate(_bonusPrefab, transform);
        orderViewSpawner.Spawn(bonus.transform);
        InitializeBonus(bonus);
        
        var targetPosition = _sparrowLandingPoint.PointForCustomers.position;
        _currentBonus.BonusMovement.MoveToTarget(targetPosition);
        yield return new WaitUntil(() => (bonus.transform.position - targetPosition).sqrMagnitude < 0.01f);
        
        _currentBonus.SwitcherStateTrigger(true);
    }
    private void InitializeBonus(Bonus bonus)
    {
        _currentBonus = bonus;
        _currentBonus.Initialize(_sparrowLandingPoint);
        _currentBonus.BonusFlewToTarget += StartSpawning;
    }

    private void StartSpawning()
    {
        StartCoroutine(SpawnObject());
        if (_orderView != null)
        {
            _currentBonus.BonusFlewToTarget -= StartSpawning;
            Destroy(_orderView.gameObject);
        }
    }
}