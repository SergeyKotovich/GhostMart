using DG.Tweening;
using Pointer;
using UnityEngine;
using UnityEngine.Animations;


public class InteractablePopup : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    [SerializeField] LookAtConstraint _lookAtConstraint;
    [SerializeField] private PointerController _pointerController;

    private void Awake()
    {
        var source = new ConstraintSource();
        source.sourceTransform = Camera.main.transform;
        source.weight = 1f;
        _lookAtConstraint.AddSource(source);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Player.Player _)) return;
        _lookAtConstraint.enabled = true;
        ShowSlowly(_canvas.transform, Vector3.one, 0.5f, null);
        
        _pointerController.HidePointer();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out Player.Player _)) return;
        _lookAtConstraint.enabled = false;
        DisappearSlowly(_canvas.transform);
    }
    
    private static void ShowSlowly(Transform transform, Vector3 targetScale, float duration, TweenCallback onComplete)
    {
        transform.gameObject.SetActive(true);
        transform.DOScale(targetScale, duration).SetEase(Ease.OutBack, 2f).OnComplete(onComplete);
    }

    private static void DisappearSlowly(Transform transform)
    {
        transform.DOScale(Vector3.one * Mathf.Epsilon, 0.2f).SetAutoKill().SetRecyclable().OnComplete(() => { transform.gameObject.SetActive(false); });
    }
}