using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    [Serializable]
    public class GridCell
    {
        [field:SerializeField]
        public Vector3 CellCoordinatesInWorld { get; private set; }
        public bool IsAvailable { get; private set; }

        public GridCell()
        {
            IsAvailable = true;
        }

        public void ChangeAvailability()
        {
            IsAvailable = !IsAvailable;
        }
    }
}