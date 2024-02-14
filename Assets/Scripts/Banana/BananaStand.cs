using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace.Banana
{
    public class BananaStand : MonoBehaviour, IStand
    {
        [SerializeField] private GridCell[] _standCells;
        
        [SerializeField] private Transform[] _positions;
        [SerializeField] private bool[] _isPositionAvalable;
        
        public bool IsAvailable { get; private set; }

        private List<GameObject> _products = new List<GameObject>();


        public void SetProductOnStand(GameObject product)
        {
            for (int i = 0; i < _positions.Length; i++)
            {
                if (_isPositionAvalable[i])
                {
                    product.transform.position = _positions[i].position;
                    product.transform.SetParent(null);

                    Vector3 rotationEuler = new Vector3(0f, -80f, -90f);
                    Quaternion rotationQuaternion = Quaternion.Euler(rotationEuler);
            
                    product.transform.rotation = rotationQuaternion;
                    
                    _products.Add(product);
                    _isPositionAvalable[i] = false;
                    return;
                }
            }
        }

        public GameObject GetAvailableProduct()
        {
            /* TODO: dictionary where the key is a product and the value is a cell's coordinates in the world
                and when a customer gets a product just to find the cell in _standCells and make IsAvailable = true
             */ 
            
            var product = _products.First();
            _products.RemoveAt(0);
            return product;
        }
    }
}