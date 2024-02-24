using Interfaces;
using UnityEngine;

public class Assistant : MonoBehaviour, ICollectable
{
    [field:SerializeField] public WorkerBasket WorkerBasket { get; private set; }
    public void PickUpProduct(Product product)
    {
        
    }

    IBasket ICollectable.WorkerBasket { get; }
}