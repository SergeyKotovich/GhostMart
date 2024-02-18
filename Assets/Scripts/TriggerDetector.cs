using System;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
       [SerializeField] private CollectingProducts _collectingProducts;
       private void OnTriggerStay(Collider other)
       {
              if (other.gameObject.CompareTag(GlobalConstants.BANANA_PLANT_TAG))
              {
                   //  _collectingProducts.TryPickUpProduct();
              }
              
       }
}