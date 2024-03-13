public interface ISleepable
{
    public bool IsSleeping { get; }
    public void SetSleepingState(bool value);
}