using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class ProductsSetter : MonoBehaviour
    {
        [SerializeField] private Grid _grid;
        private int _width = 5;
        private int _height = 4;
        private List<Vector3> _gridPoints = new List<Vector3>();
        private int _maxObject;
        private int _row;

        private void Awake()
        {
            SpawnPoints();
        }

        private void SpawnPoints()
        {

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

                    Debug.Log(_gridPoints[index]);
                
                    index++;
                    _maxObject++;
                }
            }
        }
        
    }
    
}