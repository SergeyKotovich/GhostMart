using UnityEngine;

namespace SparrowBonus
{
    [CreateAssetMenu (fileName = "MoneySpawnerConfig", menuName = "ScriptableObject/MoneySpawnerConfig")]

    public class MoneySpawnerConfig : ScriptableObject
    {
        [field:SerializeField] public Vector3 Scale { get; private set; }
        [field:SerializeField] public float AnimationDuration { get; private set; }
    }
}