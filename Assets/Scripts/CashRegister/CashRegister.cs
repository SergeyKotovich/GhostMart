using DefaultNamespace;
using Interfaces;
using UnityEngine;

namespace CashRegister
{
    public class CashRegister : IInteractable
    {
        [field:SerializeField] public Sprite StandIcon {get; private set; }
        [field:SerializeField] public Transform PointForCustomers { get; private set; }
        [field:SerializeField] public StandsTypes Type { get; private set; }
        public bool IsAvailable { get; private set; }
    }
}