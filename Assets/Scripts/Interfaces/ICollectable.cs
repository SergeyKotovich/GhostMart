using Interfaces;

public interface ICollectable
{
    public IBasket WorkerBasket { get; }
    public bool CanPickUp => !WorkerBasket.IsFull();

    public void PickUpProduct(Product product);
}