using UnityEngine;

public interface ISleepable
{
    public bool IsSleeping { get; }
    public Collider Collider { get; }
    public int MaxRepeatCount { get; }
    public void SetSleepingState(bool value);
}