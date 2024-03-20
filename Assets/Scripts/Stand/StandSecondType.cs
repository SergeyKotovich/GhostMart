using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Interfaces;
using UnityEngine;

public class StandSecondType : MonoBehaviour, IStand, IInteractable , IStorageable
{
    [field:SerializeField] public Sprite Icon {get; private set; }
    [field:SerializeField] public Transform PointForCustomers { get; private set; }
    
    [field:SerializeField] public TypeProduct TypeProduct { get; private set; }
    [field: SerializeField] public InteractableTypes Type { get; private set; }
    public List<StandCell> StandCells { get; } = new();
    public bool IsAvailable { get; private set; }
    
    [SerializeField] private Grid _grid;
    
    [SerializeField] private Transform _dropPoint;
    
    private int _width = 3;
    private int _height = 3;
    private int _length = 2;
    private void Awake()
    {
        FillAvailablePositions();
        IsAvailable = true;
    }

    public bool IsFull()
    {
        return StandCells.All(standCell => !standCell.IsAvailable);
    }

    public void AddProduct(Product product)
    {
        for (int i = 0; i < StandCells.Count; i++)
        {
            if (StandCells[i].IsAvailable)
            {
                product.transform.DOLocalMove(StandCells[i].CellPositionInWorld, 0.6f);
                product.transform.SetParent(null);
               // product.transform.DOPunchScale(new Vector3(4, 4, 2), 0.2f);

                Vector3 rotationEuler = new Vector3(-90, 0, 0);
                Quaternion rotationQuaternion = Quaternion.Euler(rotationEuler);

                product.transform.rotation = rotationQuaternion;

                StandCells[i].SetProductInCell(product);
                return;
            }
        }
    }
    
    public int GetProductsCount()
    {
        var counter = 0;
        if (StandCells.Count == 0) return 0;

        for (int i = 0; i < StandCells.Count; i++)
        {
            if (!StandCells[i].IsAvailable)
            {
                counter++;
            }
        }

        return counter;
    }

    public Product GetAvailableProduct()
    {
        for (int i = StandCells.Count - 1; i >= 0; i--)
        {
            if (!StandCells[i].IsAvailable)
            {
                return StandCells[i].GetProductFromCell();
            }
        }
        return null;
    }

    public bool IsEmpty()
    {
        foreach (var standCell in StandCells)
        {
            if (!standCell.IsAvailable)
            {
                return false;
            }
        }
        
        return true;
    }
    private void FillAvailablePositions()
    {
        if (_grid == null)
        {
            return;
        }
        
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int z = 0; z < _length; z++)
                {
                    Vector3Int gridPosition = new Vector3Int(y, x, z);
                    Vector3 worldCenterPosition = _grid.GetCellCenterWorld(gridPosition);

                    StandCell newCell = new StandCell(worldCenterPosition);
                    StandCells.Add(newCell);
                }
            }
        }
    }
    
    public Transform GetDropPoint()
    {
        return _dropPoint;
    }

    
}