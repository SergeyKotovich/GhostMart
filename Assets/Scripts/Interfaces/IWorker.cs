using Interfaces;

public interface IWorker
{
    public IWorkerBasket Basket { get; } 
    public bool CanPickUp => !Basket.IsFull();
    
    public  void PickUpProduct(Product product);
    
}