using Interfaces;
using UnityEngine;

namespace Customer
{
    public interface IOrder
    {
        public IInteractable Target { get; }
        public int MaxCount { get; }
        public int CurrentCount { get;  }
        public Vector3 TargetPosition => Target.PointForCustomers.position;
        public InteractableTypes TargetType => Target.Type;
        public bool IsCompleted => CurrentCount == MaxCount;

        public void OnGotProduct();
    }
}