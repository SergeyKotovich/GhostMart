using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

public class BasketController : MonoBehaviour
{
    [FormerlySerializedAs("_basket")] [SerializeField] private WorkerBasket workerBasket;
    [SerializeField] private CollectingProducts _collectingProducts;
    public void GetProducts( IFactory productFactory)
    {
        if (workerBasket.IsFull() || !productFactory.HasSpawnedProduct())
        {
            return;
        }
        var product = productFactory.GetProduct();
        
        if (product!=null)
        {
            workerBasket.AddProductInBasket(product);
            _collectingProducts.SetPosition(product);
        }
    }

    public void AddProductInBasket(Product product)
    {
        workerBasket.AddProductInBasket(product);
    }
}