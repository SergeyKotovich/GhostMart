using System;
using System.Collections.Generic;
using Customer;

namespace Interfaces
{
    public interface ICustomer
    {
        public int CurrentPathIndex { get; set; }
        public ProductBarView _productBarView { get; }
        public List<ListItem> ShoppingList { get; }
        
        public void Initialize(List<Stand> path, ProductBarView productBarView);
    }
}