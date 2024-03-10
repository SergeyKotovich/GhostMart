using UnityEngine;

namespace Assistant
{
    public class AssistantController : MonoBehaviour
    {
        private void Start()
        {
            var stateMachine = new StateMachine
            (
                GetComponent<MovingToTargetState>(),
                GetComponent<CollectingProductsState>(),
                GetComponent<ProductStandState>(),
                GetComponent<RecyclingProductsState>()
            );

            stateMachine.Initialize();
            stateMachine.Enter<MovingToTargetState>();
        }
    }
}