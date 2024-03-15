using UnityEngine;
using UnityEngine.Serialization;

namespace Island
{
    [CreateAssetMenu (fileName = "IslandControllerConfig", menuName = "ScriptableObject/IslandControllerConfig")]
    public class IslandControllerConfig : ScriptableObject
    {
        [field:SerializeField] public float PercentageIncrease { get; private set; }
        [field:SerializeField] public float Duration { get; private set; }
    }
}