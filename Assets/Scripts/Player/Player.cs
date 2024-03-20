using System.Collections.Generic;
using Events;
using Interfaces;
using SimpleEventBus.Events;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Player
{
    public class Player : MonoBehaviour, IWorker
    { 
        [field: SerializeField] public WorkerTypes Type { get; private set; }
        [field: SerializeField] public Wallet Wallet { get; private set; }
        [field: SerializeField] public CollectingProducts CollectingProducts { get; private set; }
        [field: SerializeField] public AbilitiesController AbilitiesController { get; private set; }
        public IWorkerBasket Basket { get; private set; }
        public bool CanPickUp => !Basket.IsFull();
        public bool HasProducts => !Basket.IsEmpty();


        private void Awake()
        {
            Basket = GetComponent<IWorkerBasket>();
            AbilitiesController.Initialize(Basket, this);
            EventStreams.Global.Subscribe<ProductWasPickedUp>(TryPickUpProduct);
        }

        public void PickUpProduct(Product product)
        {
            Basket.AddProduct(product);
        }

        public void TryPickUpProduct(ProductWasPickedUp productWasPicked)
        {
            if (Basket.IsFull())return;
            
            productWasPicked.Product.OnProductWasPickedUp();
            Basket.AddProduct(productWasPicked.Product);
            CollectingProducts.TryToSetPosition(productWasPicked.Product);
        }

        public Product GetProduct()
        {
            return Basket.GetProduct();
        }

        public List<Product> GetAllProducts()
        {
            return Basket.GetAllProducts();
        }

        public void AddMoney(int amount)
        {
            Wallet.AddMoney(amount);
        }
    }
}