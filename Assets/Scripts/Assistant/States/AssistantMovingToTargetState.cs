using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

public class AssistantMovingToTargetState : MonoBehaviour, IState
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Animator _animator;
    [SerializeField] private TargetPositionController _targetPositionController;
    
    private readonly int IsMoving = Animator.StringToHash("IsMoving");
    private bool _isMoving;
    private StateMachine _stateMachine;
    private bool _isProductFactory;
    private bool _isStand;
    private IFactory _productFactory;
    private IStand _stand;

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
        _navMeshAgent.SetDestination(_targetPositionController.SetPosition());
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
            _stateMachine.Enter<CollectingProductsState,IFactory>(_productFactory);
        }

        if (_isStand)
        {
            _animator.SetBool(IsMoving, false);
            _stateMachine.Enter<ProductStandState, IStand>(_stand);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalConstants.PRODUCT_FACTORY_TAG))
        {
            _isProductFactory = true;
            _productFactory = other.GetComponent<IFactory>();
        }
        if (other.CompareTag(GlobalConstants.STAND_TAG))
        {
            _isStand = true;
            _stand = other.GetComponent<IStand>();
        }
    }
}