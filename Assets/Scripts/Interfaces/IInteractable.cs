using DefaultNamespace;
using UnityEngine;

namespace Interfaces
{
    public abstract class IInteractable : MonoBehaviour
    {
        public Sprite StandIcon {get; }
        public StandsTypes Type { get; }
        public Transform PointForCustomers { get; }
        public bool IsAvailable { get; }
    }
}