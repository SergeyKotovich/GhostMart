using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GridCoordinates : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           //var cellWorldPosition = _grid.CellToWorld((Input.mousePosition));
           //
           //Debug.Log(cellWorldPosition);
        }
    }
}