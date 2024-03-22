using System.Collections.Generic;
using Interfaces;
using Order;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Customer
{
    public class Customer : MonoBehaviour, ICustomer
    {
        [SerializeField] private MovementController.MovementController _movementController;
        public List<IOrder> OrdersList { get; } = new();
        public Vector3 PositionInLine { get; private set; }
        public int ProductsCountInBasket => _basket.ProductsCount;
        private ICustomerBasket _basket;

        private void Start()
        {
            var stateMachine = new StateMachine
            (
                GetComponent<MovingToTargetState>(),
                GetComponent<PayingProductsState>(),
                GetComponent<GettingProductsState>(),
                GetComponent<AtCashRegisterState>()
            );
            
            stateMachine.Initialize();
            stateMachine.Enter<MovingToTargetState>();
        }
        
        public void Initialize(List<IInteractable> path)
        {
            foreach (var stand in path)
            {
                var count = Random.Range(1, 5);
                var stopPoint = new CurrentOrder(stand, count);
                OrdersList.Add(stopPoint);
            }
            
            _basket = new CustomerBasket();
        }

        public void SetDestination(Vector3 position)
        {
            _movementController.SetDestination(position);
            PositionInLine = position;
        }

        public bool IsAtTargetPoint()
        {
            return _movementController.IsAtTargetPoint();
        }

        public void AddProductInBasket(Product product)
        {
            _basket.AddProduct(product);
        }

        public List<Product> GetBoughtProducts()
        {
            return _basket.BoughtProducts;
        }

    }
}