using UnityEngine;

namespace DefaultNamespace
{
    public abstract class IProduct : MonoBehaviour
    {
        protected abstract int Price { get; set; }
    }
}