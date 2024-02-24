using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AssistantMovingToTargetState : MonoBehaviour, IState
{
    
    [SerializeField] private Assistant _assistant;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Animator _animator;
    [SerializeField] private List<Transform> _pointPath;

    private static readonly int _isMoving = Animator.StringToHash("IsMoving");
    private StateMachine _stateMachine;
    private bool _isProductFactory;
    private bool _isStand;
    private int _currentIndex;

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    private void Update()
    {
        if (_navMeshAgent.remainingDistance <0.1f)
        {
            EnterNextState();
            StopMoving();
            _isStand = false;
            _isProductFactory = false;
        }
    }

    private void MoveToPoint()
    {
        _animator.SetBool(_isMoving, true);
        _navMeshAgent.SetDestination(_pointPath[_currentIndex].position);

        SetNextPoint();
    }
    private void StopMoving()
    {
        _animator.SetBool(_isMoving, false);
    }

    private void SetNextPoint()
    {
        _currentIndex = (_currentIndex + 1) % _pointPath.Count;
    }
    public void OnEnter()
    {
        MoveToPoint();
    }

    private void EnterNextState()
    {
        if (_isProductFactory)
        {
            _stateMachine.Enter<CollectingProductsState>();
        }

        if (_isStand)
        {
            _stateMachine.Enter<ProductStandState>();
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalConstants.PRODUCT_FACTORY_TAG))
        {
            _isProductFactory = true;
        }
        if (other.CompareTag(GlobalConstants.STAND))
        {
            _isStand = true;
        }
    }
}
