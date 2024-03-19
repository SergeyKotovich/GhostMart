using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

namespace BadCustomer
{
    public class PathCreator : MonoBehaviour
    {
        [SerializeField] private Stand[] _stands;
        [SerializeField] private StandSecondType[] _secondTypesStands;
        [SerializeField] private ExitPoint _exitPoint;

        public List<IInteractable> GetRandomPath()
        {
            List<IInteractable> availableStands = _stands.Where(stand => stand.IsAvailable).Cast<IInteractable>().ToList();

            availableStands.AddRange(_secondTypesStands.Where(stand => stand.IsAvailable));

            var randomIndex = Random.Range(0, availableStands.Count);
            return new List<IInteractable>
            {
                availableStands[randomIndex],
                _exitPoint
            };
        }
    }
}