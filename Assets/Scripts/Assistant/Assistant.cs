using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Assistant : MonoBehaviour, ICollectable
{
    public event Action CameToTarget;
    
    private static readonly int _isMoving = Animator.StringToHash("IsMoving");
    
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Animator _animator;
    [SerializeField] private List<Transform> _pointPath;
    [field:SerializeField] public Basket Basket { get; private set; }
    private int _currentIndex;
    

    private void Update()
    {
        if (_navMeshAgent.remainingDistance <0.1f)
        {
            CameToTarget?.Invoke();
            StopMoving();
        }
    }

    public void MoveToPoint()
    {
        _navMeshAgent.SetDestination(_pointPath[_currentIndex].position);
        _animator.SetBool(_isMoving, true);
        SetNextPoint();
    }
    public void StopMoving()
    {
        _animator.SetBool(_isMoving, false);
    }

    private void SetNextPoint()
    {
        _currentIndex = (_currentIndex + 1) % _pointPath.Count;
    }


}