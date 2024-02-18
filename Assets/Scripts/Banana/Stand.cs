using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.Banana
{
    public class Stand : MonoBehaviour, IStand
    {
        public bool IsAvailable { get; private set; }

        [SerializeField] private bool[] _isPositionAvalable;
        [SerializeField] private Grid _grid;
        [field:SerializeField] public StandsTypes Type { get; private set; }
        
        private List<Vector3> _gridPoints = new List<Vector3>();
        private List<GameObject> _products = new List<GameObject>();

        private int _width = 4;
        private int _height = 5;


        private void Awake()
        {
            SpawnPoints();
        }

        public bool SetProductOnStand(GameObject product)
        {

            if (Enum.TryParse<StandsTypes>(product.tag, out var productType))

            if (productType != Type)
            {
                return false;
            }    
            
            for (int i = 0; i < _gridPoints.Count; i++)
            {
                if (_isPositionAvalable[i])
                {
                    product.transform.position = _gridPoints[i];
                    product.transform.SetParent(null);
                    product.transform.DOPunchScale(new Vector3(4, 4, 2), 0.2f);

                    Vector3 rotationEuler = new Vector3(0f, -80f, -90f);
                    Quaternion rotationQuaternion = Quaternion.Euler(rotationEuler);
            
                    product.transform.rotation = rotationQuaternion;
                    
                    _products.Add(product);
                    _isPositionAvalable[i] = false;
                    return true;
                }
            }

            return false;
        }

        public GameObject GetAvailableProduct()
        {
            var product = _products.First();
            _products.RemoveAt(0);
            return product;
        }
        
        private void SpawnPoints()
        {
            int index = 0;

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    Vector3Int gridPosition = new Vector3Int(x, 0, y);
                    Vector3 worldCenterPosition = _grid.GetCellCenterWorld(gridPosition);
                    _gridPoints.Add(worldCenterPosition);

                    //Debug.Log(_gridPoints[index]);
                
                    index++;
                }
            }
        }
    }
}