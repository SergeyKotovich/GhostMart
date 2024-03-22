public interface IMovable
{
    public MovementController.MovementController MovementController { get;}
    public bool IsAtDestination();
}