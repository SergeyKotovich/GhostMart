using UnityEngine;

namespace DefaultNamespace.Banana
{
    public class Banana : IProduct
    {
        protected override int Price { get;  set; }
    }
}