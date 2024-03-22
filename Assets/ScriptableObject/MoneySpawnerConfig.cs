using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu (fileName = "MoneySpawnerConfig", menuName = "ScriptableObject/MoneySpawnerConfig")]

public class MoneySpawnerConfig : ScriptableObject
{
    [field:SerializeField] public int Width { get; private set; } 
    [field:SerializeField] public int Height { get; private set; }
    [field:SerializeField] public int Length { get; private set; }
    [field:SerializeField] public int MoneyStep { get; private set; }
    [field:SerializeField] public int MaxRow { get; private set; }
    [field:SerializeField] public float NewYPosition { get; private set; }
    [field:SerializeField] public float AnimationDuration { get; private set; }
}