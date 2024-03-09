using UnityEngine;

public class ProductBarSpawner : MonoBehaviour
{
    [SerializeField] private ProductBarView _productBarPrefab;
    [SerializeField] private Canvas _canvas;

    public ProductBarView GetProductBar(GameObject character)
    {
        var productBarView = Instantiate(_productBarPrefab, _canvas.transform);
        
        var uIElementPositionController = productBarView.GetComponent<UIElementPositionController>();
        uIElementPositionController.Initialize(character.transform);
        return productBarView;
    }
}