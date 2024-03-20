using Interfaces;
using UnityEngine;

namespace Assistant
{
    public class ProductStandState : MonoBehaviour, IPayLoadedState<IStorageable>
    {
        private StateMachine _stateMachine;
        
        private IWorker _assistant;
        private IStorageable _stand;
        private ISleepable _sleepController;

        private int _countRepeatBeforeSleep;

        private void Awake()
        {
            _assistant = GetComponent<IWorker>();
            _sleepController = GetComponent<ISleepable>();
        }
        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter(IStorageable payload)
        {
            _stand = payload;
            if (!_assistant.Basket.IsEmpty())
            {
                if (_stand.IsFull())
                {
                    _stateMachine.Enter<MovingToTargetState,TypeInteractablePoints>(TypeInteractablePoints.Recycling);
                }
                else
                {
                    SetProductOnStand();
                }
            }
        }
        
        private void SetProductOnStand()
        {
            while (!_assistant.Basket.IsEmpty())
            {
                if (_stand.IsFull())
                {
                    _stateMachine.Enter<MovingToTargetState,TypeInteractablePoints>(TypeInteractablePoints.Recycling);
                    return;
                }

                var product = _assistant.Basket.GetProduct();
                _stand.AddProduct(product);
            }

            if (_assistant.Basket.IsEmpty())
            {
                if (_countRepeatBeforeSleep >= _sleepController.MaxRepeatCount)
                {
                    _countRepeatBeforeSleep = 0;
                    _stateMachine.Enter<MovingToTargetState, TypeInteractablePoints>(TypeInteractablePoints.SleepPoint);
                    return;
                }
                _countRepeatBeforeSleep++;
                _stateMachine.Enter<MovingToTargetState, TypeInteractablePoints>(TypeInteractablePoints.ProductFactory);
            }
        }
    }
}