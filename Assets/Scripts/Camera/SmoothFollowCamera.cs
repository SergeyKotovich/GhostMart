using UnityEngine;

public class SmoothFollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothSpeed = 0.125f;
    private Vector3 _velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (_target == null) return;

        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 desiredPosition = _target.position + _offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, _smoothSpeed);
        
        transform.position = smoothedPosition;
        transform.LookAt(_target);
    }
}