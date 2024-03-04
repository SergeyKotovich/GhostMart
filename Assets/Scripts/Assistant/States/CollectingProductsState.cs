using Interfaces;
using UnityEngine;

public class CollectingProductsState : MonoBehaviour , IPayLoadedState<IFactory>
{
    [SerializeField] private CollectingProducts _collectingProducts;
    
    private IWorker _assistant;
    private StateMachine _stateMachine;
    private bool _canPickUp;
    private IFactory _productFactory;

    private void Awake()
    {
        _assistant = GetComponent<IWorker>();
    }

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    
    private void Update()
    {
        if (_canPickUp)
        {
            CollectingProduct();
        }
    }

    private void CollectingProduct()
    {
        if (!_productFactory.HasSpawnedProduct())
        {
            return;
        }
        
        if (_assistant.Basket.IsFull())
        {
            _canPickUp = false;
            _stateMachine.Enter<AssistantMovingToTargetState>();
            return;
        }
        
        var product = _productFactory.GetProduct();
        _assistant.PickUpProduct(product);
        _collectingProducts.SetPosition(product);
    }

    public void OnEnter(IFactory payload)
    {
        _canPickUp = true;
        _productFactory = payload;
    }
}