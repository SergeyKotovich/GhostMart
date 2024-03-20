using UnityEngine;

namespace Interfaces
{
    public interface IInteractable
    {
        public Sprite Icon {get; }
        public InteractableTypes Type { get; }
        public Transform PointForCustomers { get; }
        public bool IsAvailable { get; }

    }
}