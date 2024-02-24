using UnityEngine;

public class BasketController : MonoBehaviour
{
    [SerializeField] private Basket _basket;
    [SerializeField] private CollectingProducts _collectingProducts;
    public void GetProducts( ProductFactory productFactory)
    {
        if (_basket.IsFull())
        {
            return;
        }
        var product = productFactory.GetProduct();
        
        if (product!=null)
        {
            _basket.AddProductInBasket(product);
            _collectingProducts.SetPosition(product);
        }
    }
}