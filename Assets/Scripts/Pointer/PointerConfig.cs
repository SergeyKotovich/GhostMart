using UnityEngine;

namespace Pointer
{
    [CreateAssetMenu (fileName = "PointerConfig", menuName = "ScriptableObject/PointerConfig")]
    public class PointerConfig : ScriptableObject
    {
        [field:SerializeField]
        public float OffsetY { get; private set; }
        
        [field:SerializeField]
        public float AnimationDelay { get; private set; }
        
        [field:SerializeField]
        public int DelayBeforeNextTarget { get; private set; }
    }
}