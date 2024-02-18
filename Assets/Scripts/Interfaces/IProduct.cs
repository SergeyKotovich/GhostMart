using UnityEngine;

namespace Interfaces
{
    public abstract class IProduct : MonoBehaviour
    {
        protected abstract int Price { get; set; }
    }
}