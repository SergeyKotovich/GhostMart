public interface IRecyclable
{
    public bool ISRecycling { get; }
    public void SetRecyclingState(bool value);
}