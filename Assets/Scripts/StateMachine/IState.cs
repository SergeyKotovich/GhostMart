public interface IState
{
    public void Initialize(StateMachine stateMachine);

    public void OnEnter();
    
}