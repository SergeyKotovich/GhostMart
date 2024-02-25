using System;
using Interfaces;
using UnityEngine;

public class Player : MonoBehaviour, IWorker
{
    public IWorkerBasket Basket { get; private set; }
    public bool CanPickUp => !Basket.IsFull();
    
  private void Awake()
  {
      Basket = GetComponent<IWorkerBasket>();
  }

  
  public void PickUpProduct(Product product)
    {
        Basket.AddProductInBasket(product);
    }
}