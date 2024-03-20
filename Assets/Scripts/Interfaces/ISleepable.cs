using UnityEngine;

public interface ISleepable
{
    public int MaxRepeatCount { get; }
    public void ImproveRepetitionCount(int value);
    public void FellAsleep();
    public void WakeUp();
}