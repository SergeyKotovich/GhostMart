using UnityEngine;

public class Player : MonoBehaviour, ICollectable
{
    [field: SerializeField] public Basket Basket { get; private set; }
   
}