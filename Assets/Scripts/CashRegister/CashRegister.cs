using Interfaces;
using UnityEngine;

namespace CashRegister
{
    public class CashRegister : IInteractable
    {
        [field:SerializeField] public Sprite StandIcon {get; private set; }
        [field:SerializeField] public Transform PointForCustomers { get; private set; }
        [field:SerializeField] public TypeProduct Type { get; private set; }
        public bool IsAvailable { get; private set; }
    }
}