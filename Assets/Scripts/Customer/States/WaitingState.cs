using System.Collections;
using UnityEngine;

namespace Customer
{
    public class WaitingState : MonoBehaviour, IState
    {
        private StateMachine _stateMachine;

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {
            StartCoroutine(WaitCoroutine());
        }
        
        public void OnExit()
        {
            
        }
        
        private IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(5);
            _stateMachine.Enter<MovingToTargetState>();
        }
    }
}