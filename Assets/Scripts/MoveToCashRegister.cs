using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class MoveToCashRegister : MonoBehaviour
{
    [SerializeField] private MovementController.MovementController _movementController;
    [SerializeField] private Transform _pointForCashRegister;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    void Start()
    {
        Move();
    }

    private async UniTask Move()
    {
        gameObject.transform.DOLocalMove(new Vector3(-18f, 0f, 11f), 7f);
        await UniTask.Delay(5000);
        _movementController.enabled = true;
        _navMeshAgent.enabled = true;
        _movementController.SetDestination(_pointForCashRegister.transform.position);
    }
}