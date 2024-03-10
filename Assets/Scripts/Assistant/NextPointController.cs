using UnityEngine;

namespace Assistant
{
    public class NextPointController : MonoBehaviour
    {
        [SerializeField] private StandController _standController;
        
        public FactoryStand GetRandomNextPoint()
        {
            var availableFactoryStands =  _standController.GetAvailableFactoryStands();
            var randomIndex = Random.Range(0, availableFactoryStands.Count);
            
            return  availableFactoryStands[randomIndex];
        }
    }
}