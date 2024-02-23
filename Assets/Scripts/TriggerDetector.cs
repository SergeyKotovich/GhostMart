using System;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private CollectingProducts _collectingProducts;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GlobalConstants.PRODUCT_FACTORY_TAG))
        {
            if (_player.Basket.IsFull())
            {
                return;
            }
            
            var productFactory = other.GetComponent<ProductFactory>();
            Debug.Log(productFactory);
            var product = productFactory.GetProduct();
            if (product!=null)
            {
                _player.Basket.AddProductInBasket(product);
                _collectingProducts.SetPosition(product);
            }

        }
        
        if (other.gameObject.CompareTag(GlobalConstants.STAND))
        {
         //  var product = _collectingProducts.TryGetProduct();
         //  if (product == null) return;
         //  
         //  var stand = other.gameObject.GetComponent<Stand>();

         //  if (!stand.SetProductOnStand(product))
         //  {
         //      _collectingProducts.PickUpProduct(product);
         //  }
        }
              
    }
}