using UnityEngine;

[CreateAssetMenu (fileName = "PlayerConfig", menuName = "ScriptableObject/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public Vector3 EyeSizeInDream { get; private set; } 
    [field: SerializeField] public Vector3 EyeSizeInWakefulness { get; private set; } 
    [field: SerializeField] public int Duration { get; private set; }
}