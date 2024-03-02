using Interfaces;

public interface IState : IInitializable
{
    public void Initialize(StateMachine stateMachine);

    public void OnEnter();
    
}