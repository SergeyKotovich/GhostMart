using System;
using UnityEngine;

[Serializable]
public class FactoryStand 
{
    [field:SerializeField] public ProductFactory ProductFactory { get; private set; }
    [field:SerializeField] public Stand Stand { get; private set; }
}