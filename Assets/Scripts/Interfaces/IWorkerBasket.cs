using System.Collections.Generic;

namespace Interfaces
{
    public interface IWorkerBasket : IBasket
    {
        public bool IsFull();
        public bool IsEmpty();
    }
}