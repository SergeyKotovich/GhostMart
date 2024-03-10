using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Customer
{
    public class PathCreator : MonoBehaviour
    {
        [SerializeField] private Stand[] _stands;
        [SerializeField] private CashRegister _cashRegister;
        [SerializeField] private ExitPoint _exitPoint;
        
        public  List<IInteractable> GetRandomPath()
        {
            List<IInteractable> availableStands = new List<IInteractable>();

            for (int i = 0; i < _stands.Length; i++)
            {
                if (_stands[i].IsAvailable)
                {
                    availableStands.Add(_stands[i]);
                }
            }

            var randomTargetsCount = Random.Range(1, availableStands.Count + 1);
            List<IInteractable> path = new List<IInteractable>();

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
            path.Add(_exitPoint);
            
            return path;
        }

    }
}