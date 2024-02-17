using UnityEngine;

[CreateAssetMenu (fileName = "SpawnerConfig", menuName = "ScriptableObject/SpawnerConfig")]
public class SpawnerConfig : ScriptableObject
{
  [field: SerializeField] public GameObject Prefab { get; private set; }
  [field: SerializeField] public Vector3 ScaleProductAfterSpawn { get; private set; }
  [field: SerializeField] public float SizeChangeTime { get; private set; }
}
