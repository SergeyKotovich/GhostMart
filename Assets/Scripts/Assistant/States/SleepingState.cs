using UnityEngine;

namespace Assistant
{
    public class SleepingState : MonoBehaviour, IState
    {
        private ISleepable _sleepController;
        private StateMachine _stateMachine;

        private bool _isActive;

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
            _isActive = true;
            _sleepController.FellAsleep();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GlobalConstants.PLAYER_TAG) && _isActive)
            {
                _isActive = false;
                _sleepController.WakeUp();
                _stateMachine.Enter<MovingToTargetState, TypeInteractablePoints>(TypeInteractablePoints.ProductFactory);
            }
        }
    }
}