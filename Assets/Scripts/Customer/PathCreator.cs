using System.Collections.Generic;
using Banana;
using UnityEngine;

namespace Customer
{
    public class PathCreator : MonoBehaviour
    {
        [SerializeField] private Stand[] _stands;
        [SerializeField] private Transform _exitTransform;
        
        public  Vector3[] GetRandomPath()
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
            List<Vector3> path = new List<Vector3>();

            for (int i = 0; i < randomTargetsCount; i++)
            {
                var randomIndex = Random.Range(0, randomTargetsCount);

                if (!path.Contains(availableStands[randomIndex].Position.position))
                {
                    path.Add(availableStands[randomIndex].Position.position);
                }
                else
                {
                    i--;
                }
            }

            path.Add(_exitTransform.position);
            
            // TODO: temp code, replace by using an array, not collections
            Vector3[] pathArray = new Vector3[path.Count];
            for (int i = 0; i < pathArray.Length; i++)
            {
                pathArray[i] = path[i];
            }
            
            return pathArray;
        }

    }
}