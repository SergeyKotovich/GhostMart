using System;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using Interfaces;
using JetBrains.Annotations;
using Stand;
using UnityEngine;

namespace Banana
{
    public class Stand : MonoBehaviour, IStand
    {
        [SerializeField] private Grid _grid;
        [field:SerializeField] public Transform Position { get; private set; }
        [field:SerializeField] public StandsTypes Type { get; private set; }
        public bool IsAvailable { get; private set; }
        private List<StandCell> StandCells { get; } = new();

        private int _width = 4;
        private int _height = 5;
        
        private void Awake()
        {
            FillAvailablePositions();
            IsAvailable = true;
        }

        public bool SetProductOnStand(GameObject product)
        {
            if (Enum.TryParse<StandsTypes>(product.tag, out var productType))

                if (productType != Type)
                {
                    return false;
                }    
            
            for (int i = 0; i < StandCells.Count; i++)
            {
                if (StandCells[i].IsAvailable)
                {
                    product.transform.position = StandCells[i].CellPositionInWorld;
                    product.transform.SetParent(null);
                    product.transform.DOPunchScale(new Vector3(4, 4, 2), 0.2f);

                    Vector3 rotationEuler = new Vector3(0f, -80f, -90f);
                    Quaternion rotationQuaternion = Quaternion.Euler(rotationEuler);
            
                    product.transform.rotation = rotationQuaternion;
                    
                    StandCells[i].SetProductInCell(product);
                    return true;
                }
            }

            return false;
        }

        [CanBeNull]
        public GameObject GetAvailableProduct()
        {
            for (int i = StandCells.Count - 1; i >= 0; i++)
            {
                if (!StandCells[i].IsAvailable)
                {
                    return StandCells[i].GetProductFromCell();
                }
            }
            return null;
        }
        
        private void FillAvailablePositions()
        {
            var index = 0;
            
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    Vector3Int gridPosition = new Vector3Int(x, 0, y);
                    Vector3 worldCenterPosition = _grid.GetCellCenterWorld(gridPosition);
                    
                    StandCell newCell = new StandCell(worldCenterPosition);
                    StandCells.Add(newCell);
                    
                    index++;
                }
            }
        }
    }
}