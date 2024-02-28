using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

public class AssistantMovingToTargetState : MonoBehaviour, IState
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Animator _animator;
    [SerializeField] private List<Transform> _pointPath;

    private bool _isMoving;
    private StateMachine _stateMachine;
    private bool _isProductFactory;
    private bool _isStand;
    private int _currentIndex;
    private readonly int IsMoving = Animator.StringToHash("IsMoving");

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    private void Update()
    {
        if (!(_navMeshAgent.remainingDistance < 0.1f)) return;
        EnterNextState();
        _isStand = false;
        _isProductFactory = false;
    }

    private void MoveToPoint()
    {
        _isMoving = true;
        _animator.SetBool(IsMoving, _isMoving);
        _navMeshAgent.SetDestination(_pointPath[_currentIndex].position);

        SetNextPoint();
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
            _animator.SetBool(IsMoving, false);
            _stateMachine.Enter<CollectingProductsState>();
        }

        if (!_isStand) return;
        _animator.SetBool(IsMoving, false);
        _stateMachine.Enter<ProductStandState>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalConstants.PRODUCT_FACTORY_TAG))
        {
            _isProductFactory = true;
        }
        if (other.CompareTag(GlobalConstants.STAND_TAG))
        {
            _isStand = true;
        }
    }
}
