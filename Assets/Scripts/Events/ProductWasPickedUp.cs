using SimpleEventBus.Events;

namespace Events
{
    public class ProductWasPickedUp : EventBase
    {
        public Product Product { get; }

        public ProductWasPickedUp (Product product)
        {
            Product = product;
        }
    }
}