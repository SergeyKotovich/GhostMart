using UnityEngine;

namespace Assistant
{
    public class SleepingState : MonoBehaviour, IState
    {
        private ISleepable _sleepController;
        private StateMachine _stateMachine;

        private void Awake()
        {
            _sleepController = GetComponent<ISleepable>();
        }

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void OnEnter()
        {
            _sleepController.FellAsleep();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GlobalConstants.PLAYER_TAG))
            {
                _sleepController.WakeUp();
                _stateMachine.Enter<MovingToTargetState, TypeInteractablePoints>(TypeInteractablePoints.ProductFactory);
            }
        }
    }
}