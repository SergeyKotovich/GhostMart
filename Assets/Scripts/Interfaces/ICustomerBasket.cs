using System.Collections.Generic;

namespace Interfaces
{
    public interface ICustomerBasket : IBasket
    {
        public List<Product> BoughtProducts { get; }

        public void ResetCurrentProductCount();
        public int GetProductsCount();

    }
}