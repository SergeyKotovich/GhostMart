using UnityEngine;
using UnityEngine.Serialization;

public class OrderViewSpawner : MonoBehaviour
{
    
    [SerializeField] private OrderView orderPrefab;
    [SerializeField] private Canvas _canvas;

    public void Spawn(Transform characterTransform)
    {
        var productBarView = Instantiate(orderPrefab, _canvas.transform);
        
        var uIElementPositionController = productBarView.GetComponent<UIElementPositionController>();
        uIElementPositionController.Initialize(characterTransform);
        productBarView.Initialize(characterTransform);
    }
}