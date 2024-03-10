using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StandController : MonoBehaviour
{
    [SerializeField] private List<FactoryStand>  _factoryStands;

    public List<FactoryStand> GetAvailableFactoryStands()
    {
        return _factoryStands.Where(factoryStand => factoryStand.Stand.IsAvailable).ToList();
    }
}