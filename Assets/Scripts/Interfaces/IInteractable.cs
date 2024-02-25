using UnityEngine;

namespace Interfaces
{
    public interface IInteractable
    {
        public Sprite StandIcon {get; }
        public TypeProduct Type { get; }
        public Transform PointForCustomers { get; }
        public bool IsAvailable { get; }

    }
}