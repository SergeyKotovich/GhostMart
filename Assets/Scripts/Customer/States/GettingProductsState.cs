
namespace Customer
{
    public class GettingProductsState : IState
    {
        private StateMachine _stateMachine;

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}