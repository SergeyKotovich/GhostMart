using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Assistant
{
    public class Assistant : MonoBehaviour, IWorker,ISleepable,IMovable,IRecyclable
    {
        [field: SerializeField] public WorkerTypes Type { get; private set; }
        public MovementController.MovementController MovementController { get; private set; }
        public AbilitiesController AbilitiesController { get; private set; }
        public IWorkerBasket Basket { get; private set; }
        public bool IsSleeping { get; private set; }
        public bool ISRecycling { get; private set; }

       
        
        private void Awake()
        {
            MovementController = GetComponent<MovementController.MovementController>();
            Basket = GetComponent<IWorkerBasket>();
            AbilitiesController = new AbilitiesController(Basket, this);
        }

        public void PickUpProduct(Product product)
        {
            Basket.AddProductInBasket(product);
        }

        public Product GetProduct()
        {
            return Basket.GetProduct();
        }

        public List<Product> GetAllProducts()
        {
            List<Product> allProducts = new List<Product>();
            var productsCount = Basket.CurrentCountProduct;
            for (int i = 0; i < productsCount; i++)
            {
                allProducts.Add(Basket.GetProduct());
            }

            return allProducts;
        }

        public void SetSleepingState(bool value)
        {
            IsSleeping = value;
        }

        public void SetRecyclingState(bool value)
        {
            ISRecycling = value;
        }
        
    }
}