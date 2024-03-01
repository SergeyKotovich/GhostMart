using UnityEngine;

namespace Interfaces
{
    public interface IInteractable
    {
        public Sprite StandIcon {get; }
        public TypeInteractablePoints TypeInteractablePoint { get; }
        public Transform PointForCustomers { get; }
        public bool IsAvailable { get; }

    }
}