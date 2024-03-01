using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class MoneySpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject _objectToSpawn; // Префаб
    [SerializeField] private Grid _grid; // Ссылка на Grid
    [SerializeField] private MoneySpawnerConfig _moneySpawnerConfig;

    private List<Vector3> _gridPoints = new(); // Список точек сетки грида
    private int _counterObjects = 0; // Колличество заспавненных объектов
    private MoneyKeeper _moneyKeeper;
    
    private int _maxObject; // Максимальное колличество объектов которое можно спавнить
    private int _row;
    
    private readonly List<GameObject> _allSpawnedMoney = new();

    private void Start()
    {
        SpawnPoints();
        _moneyKeeper = new MoneyKeeper();
    }

    private void Update()
    {
        // Проверяем, превышено ли пороговое значение денег для спавна нового объекта
        if (_moneyKeeper.CurrentMoneyAmount >= _moneyKeeper.PreviousMoneyValue + _moneySpawnerConfig.MoneyStep)
        {
            // Спавним новый объект
            SpawnObject();

            // Обновляем предыдущее значение денег
            _moneyKeeper.PreviousMoneyValue += _moneySpawnerConfig.MoneyStep;
        }

        if (_counterObjects == _maxObject && _row<_moneySpawnerConfig.MaxRow)
        {
            _counterObjects = 0;
            _maxObject = 0;
            ClearPointToSpawn();
            ChangePositionSpawner();
            SpawnPoints();
        }
    }

    public int GetMoney()
    {
        var currentAmount = _moneyKeeper.GetMoney();
        _moneyKeeper.ResetMoney();
        
        _counterObjects = 0;
        RemoveMoneyFromTable();
        _allSpawnedMoney.Clear();
        return currentAmount;
    }
    
    public void AddMoney(int amount)
    {
        _moneyKeeper.AddMoney(amount);
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
        if (_objectToSpawn == null || _grid == null)
        {
            Debug.LogError("Не установлен префаб или Grid.");
            return;
        }

        int index = 0;

        for (int x = 0; x < _moneySpawnerConfig.Width; x++)
        {
            for (int y = 0; y < _moneySpawnerConfig.Height; y++)
            {
                // Получаем позицию сетки в мировых координатах
                Vector3Int gridPosition = new Vector3Int(x, 0, y);

                // Получаем мировые координаты центра ячейки сетки
                Vector3 worldCenterPosition = _grid.GetCellCenterWorld(gridPosition);

                // Добавляем мировые координаты центра ячейки в список точек сетки
                _gridPoints.Add(worldCenterPosition);

               // Debug.Log(_gridPoints[index]);
                
                index++;
                _maxObject++;
            }
        }

        _row++;
    }

    private void SpawnObject()
    {
        // Проверяем можем ли заспавнить объект
        if (_counterObjects != _maxObject)
        {
            Quaternion prefabRotation = _objectToSpawn.transform.rotation;

            Vector3 point = _gridPoints[_counterObjects];
            // Создаем объект на вычисленной позиции
            _allSpawnedMoney.Add(Instantiate(_objectToSpawn, point, prefabRotation));
            _counterObjects++;
        }
    }

    // Пример метода, который вызывается при получении денег

    private void ChangePositionSpawner()
    {
        Vector3 newPosition = transform.position; // Создаем копию текущей позиции
        newPosition.y += _moneySpawnerConfig.NewYPosition; // Изменяем Y координату на 0.1f

        transform.position = newPosition; // Присваиваем новую позицию
    }

    private void ClearPointToSpawn()
    {
        _gridPoints.Clear();
    }
}