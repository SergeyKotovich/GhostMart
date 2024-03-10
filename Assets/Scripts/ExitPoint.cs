using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class ExitPoint : MonoBehaviour,IInteractable
{
    [field: SerializeField] public Sprite StandIcon { get; private set;}
    [field: SerializeField] public TypeInteractablePoints TypeInteractablePoint { get; private set; }
    [field: SerializeField] public Transform PointForCustomers { get; private set;}
    public bool IsAvailable { get; }
    
    
}
