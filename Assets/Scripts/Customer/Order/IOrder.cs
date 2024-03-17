using Interfaces;
using UnityEngine;

namespace Customer
{
    public interface IOrder
    {
        public Vector3 Position { get; }
        public IInteractable Target { get; }
        public int MaxCount { get; }
        public int CurrentCount { get;  }
        public Vector3 TargetPosition => Target.PointForCustomers.position;
        public TypeInteractablePoints TargetType => Target.TypeInteractablePoint;
        public bool IsCompleted => CurrentCount == MaxCount;

        public void OnGotProduct();
    }
}