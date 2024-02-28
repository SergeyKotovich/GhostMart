using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class MoneySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn; // Префаб
    [SerializeField] private Grid _grid; // Ссылка на Grid

    [SerializeField] private int _width=5; // Ширина сетки (количество ячеек по горизонтали) 5
    [SerializeField] private int _height=5; // Высота сетки (количество ячеек по вертикали) 5

    [SerializeField] private List<Vector3> _gridPoints = new List<Vector3>(); // Список точек сетки грида
    [SerializeField] private int _counterObjects = 0; // Колличество заспавненных объектов
    [SerializeField] private int _maxObject; // Максимальное колличество объектов которое можно спавнить

    [SerializeField] private int _moneyStep = 100; // Пороговое значение денег для спавна нового объекта(Шаг начисления)
    [SerializeField] private int _getMoney = 0; // Текущее количество денег на которое совершена покупка покупателей
    [SerializeField] private int _previousMoney = 0; // Предыдущее количество денег в кассе
    [SerializeField] private int _row=0;
    [SerializeField] private int _maxRow = 5;
    [SerializeField] private float _newYposition = 0.2f; // Новая позиция спавна грида

    private List<GameObject> _allMoneyObjects = new();

    private void Start()
    {
        SpawnPoints();
    }

    private void Update()
    {
        // Проверяем, превышено ли пороговое значение денег для спавна нового объекта
        if (_getMoney >= _previousMoney + _moneyStep)
        {
            // Спавним новый объект
            SpawnObject();

            // Обновляем предыдущее значение денег
            _previousMoney += _moneyStep;
        }

        if (_counterObjects == _maxObject && _row<_maxRow)
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
        var currentAmount = _getMoney;
        _getMoney = 0;
        _previousMoney = 0;
        _counterObjects = 0;
        RemoveMoneyFromTable();
        _allMoneyObjects.Clear();
        return currentAmount;
    }

    private void RemoveMoneyFromTable()
    {
        if (_allMoneyObjects.Count == 0) return;

        foreach (var moneyObject in _allMoneyObjects)
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

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
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
            _allMoneyObjects.Add(Instantiate(_objectToSpawn, point, prefabRotation));
            _counterObjects++;
        }
    }

    // Пример метода, который вызывается при получении денег
    public void AddMoney(int amount)
    {
        _getMoney += amount;
    }

    private void ChangePositionSpawner()
    {
        Vector3 newPosition = transform.position; // Создаем копию текущей позиции
        newPosition.y += _newYposition; // Изменяем Y координату на 0.1f

        transform.position = newPosition; // Присваиваем новую позицию
    }

    private void ClearPointToSpawn()
    {
        _gridPoints.Clear();
    }
}