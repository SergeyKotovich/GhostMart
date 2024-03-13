using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private int _speed;

        void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            float rotationY = 180;

            if (horizontal != 0)
            {
                rotationY = horizontal > 0 ? 90f : -90f;
            }
            else if (vertical != 0)
            {
                rotationY = vertical > 0 ? 0f : 180f;
            }

            var newRotation = Quaternion.Euler(0f, rotationY, 0f);

            var direction = new Vector3(horizontal, 0f, vertical).normalized;
            var newPosition = transform.position + direction * _speed * Time.deltaTime;

            transform.position = newPosition;
            transform.rotation = newRotation;
        }
    }
}