using UnityEngine;
using UnityEngine.Serialization;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform _target; // Объект, за которым следует камера
    [SerializeField] private Vector3 _cameraPositions; //позиция камеры

    void LateUpdate()
    {
        if (_target != null)
        {
            Vector3 desiredPosition = _target.position + _cameraPositions;
            
            // Устанавливаем позицию камеры
            transform.position = desiredPosition;
            
            // Направляем камеру на объект
            transform.LookAt(_target);
        }
    }
}