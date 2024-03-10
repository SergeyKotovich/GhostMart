using UnityEngine;

namespace Assistant
{
    public class AssistantWaitingState : MonoBehaviour, IState
    {
        private StateMachine _stateMachine;

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {

        }

    }
}