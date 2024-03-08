using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Interfaces;
using JetBrains.Annotations;
using UnityEngine;

 public class Stand : MonoBehaviour, IInteractable, IStand
    {
        [SerializeField] private Grid _grid;
        [field:SerializeField] public Sprite StandIcon {get; private set; }
        [field:SerializeField] public Transform PointForCustomers { get; private set; }
        [field:SerializeField] public TypeProduct TypeProduct { get; private set; }
        
        [field:SerializeField] public Transform PointForAssistant { get; private set; }

        [field:SerializeField] public TypeInteractablePoints TypeInteractablePoint { get; private set; }
        public bool IsAvailable { get; private set; }
        public List<StandCell> StandCells { get; } = new();

        private int _width = 4;
        private int _height = 5;
        
        private void Awake()
        {
            FillAvailablePositions();
            IsAvailable = true;
        }

        public void SetProductOnStand(Product product)
        {
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
                    return;
                }
            }
        }

        public bool IsFull()
        {
            return StandCells.All(standCell => standCell.Product != null);
        }

        [CanBeNull]
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

        public bool IsAnyFreePlace()
        {
            for (int i = 0; i < StandCells.Count; i++)
            {
                if (StandCells[i].IsAvailable)
                {
                    return true;
                }
            }
            return false;
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
            var index = 0;
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
                    
                    index++;
                }
            }
        }
    }