using Interfaces;
using UnityEngine;

namespace SparrowBonus
{
    public class SparrowLandingPoint : MonoBehaviour, IInteractable
    {
        [field:SerializeField]
        public Sprite Icon { get; private set; }
        [field:SerializeField] 
        public InteractableTypes Type { get; private set;}
        [field:SerializeField]
        public Transform PointForCustomers { get; private set;}
        public bool IsAvailable { get; private set;}

        private void Awake()
        {
            IsAvailable = true;
        }
    }
}