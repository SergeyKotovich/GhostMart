using System;
using Events;
using UnityEngine;

public class Product : MonoBehaviour
{
   [field:SerializeField] public int Price { get; private set; }
   [field:SerializeField] public TypeProduct Type { get; private set; }
   [field:SerializeField] public Collider Collider { get; private set; }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag(GlobalConstants.PLAYER_TAG))
      {
         EventStreams.Global.Publish(new ProductWasPickedUp(this));
      }
   }
}