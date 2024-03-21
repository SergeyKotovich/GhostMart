using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Interfaces;
using UnityEngine;

public class Stand : MonoBehaviour, IInteractable, IStand, IStorageable
{
    [field:SerializeField] public Sprite Icon {get; private set; }
    [field:SerializeField] public Transform PointForCustomers { get; private set; }
    [field:SerializeField] public TypeProduct TypeProduct { get; private set; }
    [field: SerializeField] public InteractableTypes Type { get; private set; }
    public bool IsAvailable { get; private set; }
    public List<StandCell> StandCells { get; } = new();
    
    [SerializeField] private Grid _grid;
    
    [SerializeField] private Transform _dropPoint;

    private int _width = 4;
    private int _height = 5;

    private void Awake()
    {
        FillAvailablePositions();
        IsAvailable = true;
    }
    public void AddProduct(Product product)
    {
        for (int i = 0; i < StandCells.Count; i++)
        {
            if (StandCells[i].IsAvailable)
            {
                product.transform.DOLocalMove(StandCells[i].CellPositionInWorld, 0.6f);
                product.transform.SetParent(null);
                
                Vector3 rotationEuler = new Vector3(0f, -80f, -90f);
                Quaternion rotationQuaternion = Quaternion.Euler(rotationEuler);
                
                product.transform.rotation = rotationQuaternion;
                
                StandCells[i].SetProductInCell(product);
                
                return;
            }
        }
        
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
                Vector3Int gridPosition = new Vector3Int(x, 0, y);
                Vector3 worldCenterPosition = _grid.GetCellCenterWorld(gridPosition);

                StandCell newCell = new StandCell(worldCenterPosition);
                StandCells.Add(newCell);
                
            }
        }
    }

    public bool IsFull()
    {
        return StandCells.All(standCell => !standCell.IsAvailable);
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
    
    public Transform GetDropPoint()
    {
        return _dropPoint;
    }
    
}