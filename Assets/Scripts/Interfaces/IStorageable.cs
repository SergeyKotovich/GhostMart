namespace Interfaces
{
    public interface IStorageable
    {
        public TypeProduct TypeProduct { get; }
        public bool IsFull();

        public void AddProduct(Product product);
        
    }
}