using Interfaces;
using UnityEngine;

namespace Customer
{
    public class CurrentOrder : IOrder
    {
        public Vector3 Position { get; }
        public IInteractable Target { get; }
        public Vector3 TargetPosition => Target.PointForCustomers.position;
        public TypeInteractablePoints TargetType => Target.TypeInteractablePoint;
        public int MaxCount { get; }
        public int CurrentCount { get; private set; }

        public CurrentOrder(Vector3 position, IInteractable target, int count)
        {
            Position = position;
            Target = target;
            MaxCount = count;
        }

        public void OnGotProduct()
        {
            CurrentCount++;
        }

    }
}