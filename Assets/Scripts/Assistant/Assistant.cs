using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Assistant : MonoBehaviour, ICollectable
{
    [field:SerializeField] public WorkerBasket WorkerBasket { get; private set; }
}