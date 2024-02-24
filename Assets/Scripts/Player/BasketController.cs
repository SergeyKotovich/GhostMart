using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

public class BasketController : MonoBehaviour
{
     [SerializeField] private WorkerBasket workerBasket;
    [SerializeField] private CollectingProducts _collectingProducts;
    

    public void AddProductInBasket(Product product)
    {
        workerBasket.AddProductInBasket(product);
    }
}