using UnityEngine;

public class AssistantController : MonoBehaviour
{
    private void Start()
    {
        var stateMachine = new StateMachine
        (
            GetComponent<AssistantWaitingState>(),
            GetComponent<AssistantMovingToTargetState>(),
            GetComponent<CollectingProductsState>(),
            GetComponent<ProductStandState>(),
            GetComponent<RecyclingProductsState>()
        );
            
        stateMachine.Initialize();
        stateMachine.Enter<AssistantMovingToTargetState>();
    }
}