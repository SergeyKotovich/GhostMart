using UnityEngine;

public class Product : MonoBehaviour
{
   [field:SerializeField] public int Price { get; private set; }
   [field:SerializeField] public TypeProduct Type { get; private set; }
}