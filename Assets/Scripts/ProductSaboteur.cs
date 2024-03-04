using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductSaboteur : MonoBehaviour
{
    [SerializeField] private Stand[] _stands;
    [SerializeField] private Transform _exitTransform;
    [SerializeField] private ProductBarSpawner _productBarSpawner;
    public MovementController MovementController { get; private set; }
}
