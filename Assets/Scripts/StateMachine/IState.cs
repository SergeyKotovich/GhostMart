using Interfaces;

public interface IState : IInitializable
{
    public void OnEnter();
}