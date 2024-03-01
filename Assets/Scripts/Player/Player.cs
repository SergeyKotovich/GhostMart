using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IWorker
{
    public IWorkerBasket Basket { get; private set; }
    public bool CanPickUp => !Basket.IsFull();
    public bool HasProducts => !Basket.IsEmpty();
    private Wallet _wallet;
    
  private void Awake()
  {
      Basket = GetComponent<IWorkerBasket>();
      _wallet = new Wallet();
  }

  public void PickUpProduct(Product product)
  {
      Basket.AddProductInBasket(product);
  }

  public Product GetProduct()
  {
      return Basket.GetProduct();
  }

  public List<Product> GetAllProducts()
  {
      List<Product> allProducts = new List<Product>();
      var productsCount = Basket.CurrentCountProduct;
      for (int i = 0; i < productsCount; i++)
      {
          allProducts.Add(Basket.GetProduct());
      }

      return allProducts;
  }
  public void AddMoney(int amount)
  {
      _wallet.AddMoney(amount);
  }
  
}