using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Assistant
{
    public class Assistant : MonoBehaviour, IWorker,IMovable
    {
        [field: SerializeField] public WorkerTypes Type { get; private set; }
        [field: SerializeField] public CollectingProducts CollectingProducts { get; private set; }
        [field: SerializeField] public AbilitiesController AbilitiesController { get; private set; }
        public MovementController.MovementController MovementController { get; private set; }
        public IWorkerBasket Basket { get; private set; }
        
        private void Awake()
        {
            MovementController = GetComponent<MovementController.MovementController>();
            Basket = GetComponent<IWorkerBasket>();
            AbilitiesController.Initialize(Basket, this, MovementController.NavMeshAgent);
        }
        private void Start() 
        {
            var stateMachine = new StateMachine
            (
                GetComponent<SleepingState>(),
                GetComponent<MovingToTargetState>(),
                GetComponent<CollectingProductsState>(),
                GetComponent<ProductStandState>(),
                GetComponent<RecyclingProductsState>()
            );

            stateMachine.Initialize();
            stateMachine.Enter<MovingToTargetState, TypeInteractablePoints>(TypeInteractablePoints.ProductFactory);
        }
        public void PickUpProduct(Product product)
        {
            Basket.AddProduct(product);
        }

        public Product GetProduct()
        {
            return Basket.GetProduct();
        }

        public List<Product> GetAllProducts()
        {
            return Basket.GetAllProducts();
        }
    }
}