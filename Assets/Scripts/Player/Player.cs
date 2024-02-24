using UnityEngine;

public class Player : MonoBehaviour, ICollectable
{
    [field: SerializeField] public WorkerBasket WorkerBasket { get; private set; }
   
}