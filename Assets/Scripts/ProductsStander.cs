using System;
using System.Collections.Generic;
using DefaultNamespace.Banana;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class ProductsStander : MonoBehaviour
    {
        [SerializeField] private Stand[] _stands;

        public void TrySetProducts(List<GameObject> products)
        {
            for (int i = 0; i < products.Count; i++)
            {
                for (int j = 0; j < _stands.Length; j++)
                {
                    
                    if (Enum.TryParse<StandsTypes>(products[i].tag, out var productType))

                    if (productType == _stands[j].Type)
                    {
                        _stands[j].SetProductOnStand(products[i]);
                    }
                }
            }
        }
    }
}