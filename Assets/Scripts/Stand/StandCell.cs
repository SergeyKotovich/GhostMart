using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Stand
{
    [Serializable]
    public class StandCell
    {
        [field:SerializeField]
        public Vector3 CellPositionInWorld { get; private set; }
        public GameObject Product { get; private set; }
        public bool IsAvailable { get; private set; }

        public StandCell(Vector3 cellPosition)
        {
            CellPositionInWorld = cellPosition;
            IsAvailable = true;
        }

        public void ChangeAvailability()
        {
            IsAvailable = !IsAvailable;
        }

        public void SetProductInCell(GameObject product)
        {
            Product = product;
            IsAvailable = false;
        }
        
        public GameObject GetProductFromCell()
        {
            IsAvailable = true;
            return Product;
        }
    }
}