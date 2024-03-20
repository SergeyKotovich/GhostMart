using UnityEngine;

[CreateAssetMenu (fileName = "ProductConfig", menuName = "ScriptableObject/ProductConfig")]
public class ProductConfig : ScriptableObject
{
  [field: SerializeField] public Vector3 ScaleProductAfterSpawn { get; private set; }
  [field: SerializeField] public float SizeChangeTime { get; private set; }
  [field: SerializeField] public Vector3 RotationProductInBasket;
  [field: SerializeField] public Vector3 ScaleProductInbasket;
  [field: SerializeField] public int MaxCountSpawnedProduct { get; private set; }
  [field: SerializeField] public float _delayBetweenSpawnObjects { get; private set; }
}