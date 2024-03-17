using UnityEngine;
using UnityEngine.Serialization;

public class OrderViewSpawner : MonoBehaviour
{
    [SerializeField] private OrderView orderPrefab;
    [SerializeField] private Canvas _canvas;

    public OrderView GetProductBar(GameObject character)
    {
        var productBarView = Instantiate(orderPrefab, _canvas.transform);
        
        var uIElementPositionController = productBarView.GetComponent<UIElementPositionController>();
        uIElementPositionController.Initialize(character.transform);
        productBarView.Init(character.transform);
        return productBarView;
    }
}