using System;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private BasketController _basketController;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GlobalConstants.PRODUCT_FACTORY_TAG))
        {
            var productFactory = other.GetComponent<ProductFactory>();
            _basketController.GetProducts(productFactory);
        }
        
        if (other.gameObject.CompareTag(GlobalConstants.STAND))
        {
            var stand = other.gameObject.GetComponent<Stand>();
            if (!stand.IsAnyFreePlace())
            {
                return;
            }
            
            var product = _player.Basket.GetProduct();
            
           if (product == null) return;
           stand.SetProductOnStand(product);

        }
              
    }
}