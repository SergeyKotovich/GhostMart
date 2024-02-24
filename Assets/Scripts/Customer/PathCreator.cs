using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Customer
{
    public class PathCreator : MonoBehaviour
    {
        [SerializeField] private Stand[] _stands;
        [SerializeField] private Stand _cashRegister;
        [SerializeField] private Transform _exitTransform;
        
        public  List<Stand> GetRandomPath()
        {

            List<Stand> availableStands = new List<Stand>();

            for (int i = 0; i < _stands.Length; i++)
            {
                if (_stands[i].IsAvailable)
                {
                    availableStands.Add(_stands[i]);
                }
            }

            var randomTargetsCount = Random.Range(1, availableStands.Count + 1);
            List<Stand> path = new List<Stand>();

            for (int i = 0; i < randomTargetsCount; i++)
            {
                var randomIndex = Random.Range(0, randomTargetsCount);

                if (!path.Contains(availableStands[randomIndex]))
                {
                    path.Add(availableStands[randomIndex]);
                }
                else
                {
                    i--;
                }
            }
            
            path.Add(_cashRegister);
            
            return path;
        }

    }
}