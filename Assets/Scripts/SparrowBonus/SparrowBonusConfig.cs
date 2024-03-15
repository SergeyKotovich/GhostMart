using UnityEngine;

namespace SparrowBonus
{
    [CreateAssetMenu (fileName = "SparrowBonusConfig", menuName = "ScriptableObject/SparrowBonusConfig")]
    public class SparrowBonusConfig : ScriptableObject
    {
        [field:SerializeField] public Sprite TargetProductIcon { get; private set; }
        [field:SerializeField] public int MaxProductsCount { get; private set; }
        [field:SerializeField] public int MaxMoneyReword { get; private set; }
    }
}