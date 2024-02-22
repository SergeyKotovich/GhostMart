using UnityEngine;

public class UIElementPositionController : MonoBehaviour
{
    [SerializeField]
    private RectTransform _uiElement;
    [SerializeField]
    private Vector3 _ofset;

    private Transform _uiElementRoot;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void Initialize(Transform uiElementRoot)
    {
        _uiElementRoot = uiElementRoot;
    }
    private void Update()
    {
        var pointInScreenSpace = RectTransformUtility.WorldToScreenPoint(_camera, _uiElementRoot.position + _ofset);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent as RectTransform, pointInScreenSpace, null, out var localPoint);

        _uiElement.anchoredPosition = localPoint;
    }
}