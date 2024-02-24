using Interfaces;
using UnityEngine;

public class Player : MonoBehaviour, ICollectable
{
  //  [field: SerializeField] public WorkerBasket WorkerBasket { get; private set; } 
    [SerializeField] private CollectingProducts _collectingProducts;
 //   public bool CanPickUp => !WorkerBasket.IsFull();

 public IBasket WorkerBasket { get; }

 public void PickUpProduct(Product product)
    {
 //       WorkerBasket.AddProductInBasket(product);
    }
}