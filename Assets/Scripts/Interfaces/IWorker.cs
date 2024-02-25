using Interfaces;

public interface IWorker
{
    public IWorkerBasket Basket { get; } 
    public bool CanPickUp => !Basket.IsFull();
    public bool HasProducts => !Basket.IsEmpty();
    
    public  void PickUpProduct(Product product);

    public Product GetProduct();
}