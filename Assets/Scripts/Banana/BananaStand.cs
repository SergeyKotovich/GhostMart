using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace.Banana
{
    public class BananaStand : MonoBehaviour, IStand
    {
        [SerializeField] private GridCell[] _standCells;
        public bool IsAvailable { get; private set; }

        private List<IProduct> _products;


        private void SetProductOnStand(IProduct product)
        {
            for (int i = 0; i < _standCells.Length; i++)
            {
                if (_standCells[i].IsAvailable)
                {
                    product.transform.position = _standCells[i].CellCoordinatesInWorld;
                    _products.Add(product);
                    _standCells[i].ChangeAvailability();
                }
            }
        }

        private IProduct GetAvailableProduct()
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