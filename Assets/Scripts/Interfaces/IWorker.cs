using System.Collections.Generic;
using Interfaces;
using UnityEngine.AI;

public interface IWorker
{
    public IWorkerBasket Basket { get; } 
    public AbilitiesController AbilitiesController { get; }
    public WorkerTypes Type { get; }
    public CollectingProducts CollectingProducts { get; }

    public bool CanPickUp => !Basket.IsFull();
    public bool HasProducts => !Basket.IsEmpty();
    
    public  void PickUpProduct(Product product);

    public Product GetProduct();
    public List<Product> GetAllProducts();
    
}