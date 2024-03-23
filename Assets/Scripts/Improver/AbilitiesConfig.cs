using UnityEngine;

namespace Improver
{
    [CreateAssetMenu(fileName = "AbilitiesConfig", menuName = "ScriptableObject/AbilitiesConfig")]
    public class AbilitiesConfig : ScriptableObject
    {
        [field: SerializeField] public int DefaultAbilityLevel { get; private set; }
        [field: SerializeField] public int ImprovingStep { get; private set; }
    }
}