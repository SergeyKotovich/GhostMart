using System;
using Interfaces;
using UnityEngine;

public class Player : MonoBehaviour, IWorker
{
    [SerializeField] private Wallet _wallet;
    public IWorkerBasket Basket { get; private set; }
    public bool CanPickUp => !Basket.IsFull();
    public bool HasProducts => !Basket.IsEmpty();
    
  private void Awake()
  {
      Basket = GetComponent<IWorkerBasket>();
  }

  public void PickUpProduct(Product product)
  {
      Basket.AddProductInBasket(product);
  }

  public Product GetProduct()
  {
      return Basket.GetProduct();
  }
  public void AddMoney(int amount)
  {
      _wallet.AddMoney(amount);
  }
  
}