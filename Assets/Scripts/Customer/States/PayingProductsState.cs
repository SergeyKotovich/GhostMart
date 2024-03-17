using System.Collections;
using Cysharp.Threading.Tasks;
using Events;
using Interfaces;
using UnityEngine;

namespace Customer
{
    public class PayingProductsState : MonoBehaviour, IPayLoadedState<ICashRegister>
    {
        [SerializeField] private int _delay;
        private StateMachine _stateMachine;
        private ICustomer _customer;
        
        private void Awake()
        {
            _customer = GetComponent<ICustomer>();
        }
        
        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter(ICashRegister cashRegister)
        {
            PayForProducts(cashRegister);
        }

        private async  UniTask PayForProducts(ICashRegister cashRegister)
        {
            await UniTask.Delay(_delay);
            
            cashRegister.SellProducts(_customer.GetBoughtProducts());
            cashRegister.OnCustomerLeft(_customer);
            _stateMachine.Enter<MovingToTargetState>();
        }
    }
}