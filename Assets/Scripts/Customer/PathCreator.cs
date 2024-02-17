using System.Collections.Generic;
using DefaultNamespace.Banana;
using DG.Tweening;
using UnityEngine;

namespace Customer
{
    public class PathCreator : MonoBehaviour
    {
        [SerializeField] private Stand[] _stands;
       
        
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
            
            var randomTargetsCount = Random.Range(2, availableStands.Count);
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

            // TODO: temp code, replace by using arrays, not collections
            Vector3[] pathArray = new Vector3[path.Count];
            for (int i = 0; i < pathArray.Length; i++)
            {
                pathArray[i] = path[i];
            }
            
            return pathArray;
        }

    }
}