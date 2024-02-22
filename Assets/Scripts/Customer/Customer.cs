
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Customer
{
    public class Customer : MonoBehaviour
    { 
        public event Action CameToTarget;
        
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;
        
        public int CurrentPathIndex;
        private bool _isMoving;
        
        public ProductBarView _productBarView;

        public List<ListItem> ShoppingList { get; private set; } = new();

        public void Initialize(List<Stand> path, ProductBarView productBarView)
        {
            foreach (var stand in path)
            {
                var count = Random.Range(1, 3);
                var stopPoint = new ListItem(stand.PointForCustomers.position, stand, count);
                ShoppingList.Add(stopPoint);
            }

            _productBarView = productBarView;
        }

        void Update()
        {
            if (_navMeshAgent.remainingDistance < 1f && !_navMeshAgent.pathPending)
            {
                CameToTarget?.Invoke();
            }
        }

        public void MoveToNextPoint()
        {
            if (CurrentPathIndex < ShoppingList.Count)
            {
                _navMeshAgent.SetDestination(ShoppingList[CurrentPathIndex].Position);
                
                _isMoving = true;
                _animator.SetBool("IsMoving", _isMoving);
                _productBarView.UpdateProductBar(ShoppingList[CurrentPathIndex]);
                
            }
            else
            {
                _navMeshAgent.isStopped = true;
                _animator.SetBool("IsMoving", false);
            }
        }

        public void StopMoving()
        {
            _isMoving = false;
            _animator.SetBool("IsMoving", _isMoving);
        }

    }
}