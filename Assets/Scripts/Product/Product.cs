using System;
using Events;
using UnityEngine;
using UnityEngine.Serialization;

public class Product : MonoBehaviour
{
   [field:SerializeField] public int Price { get; private set; }
   [field:SerializeField] public TypeProduct Type { get; private set; }
   [SerializeField] private Collider _collider;

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag(GlobalConstants.PLAYER_TAG))
      {
         EventStreams.Global.Publish(new ProductWasPickedUp(this));
      }
   }

   public void OnProductWasPickedUp()
   {
      _collider.enabled = false;
   }
   
   public void OnProductWasDropped()
   {
      _collider.enabled = true;
   }
}