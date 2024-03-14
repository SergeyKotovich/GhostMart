using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class MoneySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _moneyPrefab;
    [SerializeField] private Grid _grid;
    [SerializeField] private MoneySpawnerConfig _moneySpawnerConfig;

    private List<Vector3> _gridPoints = new();
    private MoneyStorage _moneyStorage;
    private int _currentIndex;
    private readonly List<GameObject> _allSpawnedMoney = new();

    private void Start()
    {
        SpawnPoints();
        _moneyStorage = new MoneyStorage();
    }

    public int GetMoney()
    {
        var currentAmount = _moneyStorage.GetMoney();
        _moneyStorage.ResetMoney();
        
        _currentIndex = 0;
        RemoveMoneyFromTable();
        _allSpawnedMoney.Clear();
        return currentAmount;
    }
    
    public void AddMoney(int amount)
    {
        _moneyStorage.AddMoney(amount);
        
        for (int i = 0; i < amount; i++)
        {
            SpawnObject();
        }
    }

    private void RemoveMoneyFromTable()
    {
        if (_allSpawnedMoney.Count == 0) return;

        foreach (var moneyObject in _allSpawnedMoney)
        {
            moneyObject.transform.DOScale(Vector3.zero, 1).OnComplete(() => Destroy(moneyObject));
        }
    }

    private void SpawnPoints()
    {
        for (int x = 0; x < _moneySpawnerConfig.Width; x++)
        {
            for (int y = 0; y < _moneySpawnerConfig.Height; y++)
            {
                for (int z = 0; z < 5; z++)
                {
                    Vector3Int gridPosition = new Vector3Int(y, x, z);
                    Vector3 worldCenterPosition = _grid.GetCellCenterWorld(gridPosition);
                    _gridPoints.Add(worldCenterPosition);
                }
            }
        }
    }

    private void SpawnObject()
    {
        Quaternion prefabRotation = _moneyPrefab.transform.rotation;

        Vector3 point = _gridPoints[_currentIndex];
        _allSpawnedMoney.Add(Instantiate(_moneyPrefab, point, prefabRotation));
        _currentIndex++;
    }

}