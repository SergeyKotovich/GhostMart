using System.Collections.Generic;
using DefaultNamespace.Banana;
using UnityEngine;

namespace DefaultNamespace
{
    public class ProductsStander : MonoBehaviour
    {
        [SerializeField] private BananaStand _bananaStand;


        public void TrySetProducts(List<GameObject> products)
        {
            for (int i = 0; i < products.Count; i++)
            {
                _bananaStand.SetProductOnStand(products[i]);
            }
        }
    }
}