using Interfaces;
using UnityEngine;

namespace Order
{
    public class CurrentOrder : IOrder
    {
        public IInteractable Target { get; }
        public Vector3 TargetPosition => Target.PointForCustomers.position;
        public InteractableTypes TargetType => Target.Type;
        public int MaxCount { get; }
        public int CurrentCount { get; private set; }

        public CurrentOrder(IInteractable target, int count)
        {
            Target = target;
            MaxCount = count;
        }

        public void OnGotProduct()
        {
            CurrentCount++;
        }

    }
}