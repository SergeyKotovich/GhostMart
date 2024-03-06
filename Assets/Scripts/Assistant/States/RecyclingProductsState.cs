using System;
using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.AI;

public class RecyclingProductsState : MonoBehaviour, IState
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _pointForAssistant;
    
    private StateMachine _stateMachine;
    private readonly int IsMoving = Animator.StringToHash("IsMoving");
    private bool _isMoving;
    private CompositeDisposable _subscriptions;

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _subscriptions.Add(EventStreams.Global.Subscribe<ProductsAreRecycledEvent>(OnProductsAreRecycled));
        _isMoving = true;
        _navMeshAgent.SetDestination(_pointForAssistant.position);
        _animator.SetBool(IsMoving, _isMoving);
    }

    private void OnProductsAreRecycled(ProductsAreRecycledEvent productsAreRecycledEvent)
    {
        _stateMachine.Enter<AssistantMovingToTargetState>();
        _subscriptions.Dispose();
    }
}