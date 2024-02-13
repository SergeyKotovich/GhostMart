using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target; // Объект, за которым следует камера
    public float heightAboveTarget = 5f; // Высота камеры над объектом

    void LateUpdate()
    {
        if (target != null)
        {
            // Получаем позицию объекта по осям X и Z с учетом высоты над ним
            Vector3 desiredPosition = new Vector3(target.position.x, heightAboveTarget, target.position.z);
            
            // Устанавливаем позицию камеры
            transform.position = desiredPosition;
            
            // Направляем камеру на объект
            transform.LookAt(target);
        }
    }
}