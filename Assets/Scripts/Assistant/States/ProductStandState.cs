using Interfaces;
using UnityEngine;

namespace Assistant
{
    public class ProductStandState : MonoBehaviour, IPayLoadedState<IStand>
    {
        [SerializeField] private ProductFactory _eggFactory;

        private IWorker _assistant;
        private StateMachine _stateMachine;
        private IStand _stand;

        private void Awake()
        {
            _assistant = GetComponent<IWorker>();
        }

        public void OnEnter(IStand payload)
        {
            _stand = payload;
            if (_assistant.Basket.IsEmpty())
            {
                return;
            }

            if (_stand.IsFull())
            {
                _stateMachine.Enter<RecyclingProductsState>();
            }
            else
            {
                
               SetProductOnStand();
                //нужно проверить, есть ли у фабрики яиц продукты
                if (_eggFactory.HasSpawnedProduct())
                {
                    _stateMachine.Enter<MovingToTargetState>();
                }
            }

        }

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void SetProductOnStand()
        {
            while (!_assistant.Basket.IsEmpty())
            {
                var product = _assistant.Basket.GetProduct();
                if (_stand.TypeProduct != product.Type)
                {
                    _assistant.PickUpProduct(product);
                    return;
                }
            }

            if (_assistant.Basket.IsEmpty())
            {
                _stateMachine.Enter<MovingToTargetState>();
            }
        }
    }
}