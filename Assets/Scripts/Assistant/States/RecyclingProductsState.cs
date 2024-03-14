using System;
using UnityEngine;
using UnityEngine.AI;

namespace Assistant
{
    public class RecyclingProductsState : MonoBehaviour, IState
    {
        private StateMachine _stateMachine;
        private IDisposable _subscription;
        private IRecyclable _assistant;

        private void Awake()
        {
            _assistant = GetComponent<IRecyclable>();
        }

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {
            _subscription = EventStreams.Global.Subscribe<ProductsAreRecycledEvent>(OnProductsAreRecycled);
        }
        
        private void OnProductsAreRecycled(ProductsAreRecycledEvent productsAreRecycledEvent)
        {
            _assistant.SetRecyclingState(false);
            _stateMachine.Enter<MovingToTargetState>();
            _subscription.Dispose();
        }
    }
}