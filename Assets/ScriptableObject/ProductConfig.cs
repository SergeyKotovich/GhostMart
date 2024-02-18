using UnityEngine;

[CreateAssetMenu (fileName = "SpawnerConfig", menuName = "ScriptableObject/SpawnerConfig")]
public class ProductConfig : ScriptableObject
{
  [field: SerializeField] public GameObject Prefab { get; private set; }
  [field: SerializeField] public Vector3 ScaleProductAfterSpawn { get; private set; }
  [field: SerializeField] public float SizeChangeTime { get; private set; }
  [field: SerializeField] public Vector3 PositionProductInBasket;
  [field: SerializeField] public Vector3 RotationProductInBasket;
  [field: SerializeField] public Vector3 ScaleProductInbasket;
}
