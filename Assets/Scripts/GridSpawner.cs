using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Префаб
    public Grid grid; // Ссылка на Grid

    public int width = 5; // Ширина сетки (количество ячеек по горизонтали)
    public int height = 5; // Высота сетки (количество ячеек по вертикали)

    void Start()
    {
        SpawnObjectsOnGrid();
    }

    void SpawnObjectsOnGrid()
    {
        if (objectToSpawn == null || grid == null)
        {
            Debug.LogError("Не установлен префаб или Grid.");
            return;
        }

        Quaternion prefabRotation = objectToSpawn.transform.rotation;
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Получаем позицию сетки в мировых координатах
                Vector3Int gridPosition = new Vector3Int(x, 0, y);

                // Получаем мировые координаты центра ячейки сетки
                Vector3 worldCenterPosition = grid.GetCellCenterWorld(gridPosition);

                // Создаем объект на вычисленной позиции
                Instantiate(objectToSpawn, worldCenterPosition, prefabRotation);
            }
        }
    }
}