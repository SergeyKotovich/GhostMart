using UnityEngine;

namespace SparrowBonus
{
    [CreateAssetMenu (fileName = "SparrowBonusConfig", menuName = "ScriptableObject/SparrowBonusConfig")]
    public class SparrowBonusConfig : ScriptableObject
    {
        [field:SerializeField] public Vector3 ExitPosition { get; private set; }
        [field:SerializeField] public int MaxProductsCount { get; private set; }
        [field:SerializeField] public float AnimationDuration { get; private set; }
    }
}