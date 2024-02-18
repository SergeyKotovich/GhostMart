using Interfaces;

namespace Banana
{
    public class Banana : IProduct
    {
        protected override int Price { get;  set; }
    }
}