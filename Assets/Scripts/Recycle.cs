using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Recycle : MonoBehaviour
{
    private List<GameObject> _listAllProductsInHands;

 public void Recycling(List<GameObject> other)
 {
     _listAllProductsInHands= other;
     foreach (var product in _listAllProductsInHands)
     {
         product.transform.SetParent(transform);
         product.transform.DOLocalMove(new Vector3(-0.0890000015f,0.921000004f,0.116999999f), 0.3f);
         product.transform.DOScale(new Vector3(0, 0, 0), 0.3f);
         Destroy(product,1f);
        
     }
 }
    
}
