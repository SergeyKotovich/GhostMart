using System;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    [SerializeField] private CollectingProducts _collectingProducts;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GlobalConstants.PRODUCT_FACTORY_TAG))
        {
            if (_collectingProducts.CountAvailablePlaces==0)
            {
                return;
            }
            
            var productFactory = other.GetComponent<ProductFactory>();
            for (int i = 0; i < _collectingProducts.CountAvailablePlaces; i++)
            {
                var product = productFactory.GetProduct();
                if (product!=null)
                {
                    _collectingProducts.PickUpProduct(product);
                }
                
            }
        }
        
        if (other.gameObject.CompareTag("Stand"))
        {
            var product = _collectingProducts.TryGetProduct();
            if (product == null) return;
            
            var stand = other.gameObject.GetComponent<Stand>();

            if (!stand.SetProductOnStand(product))
            {
                _collectingProducts.PickUpProduct(product);
            }
        }
              
    }
}