using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    [SerializeField]
    private int _speed;

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        
        var direction = new Vector3(horizontal, 0f, vertical);
        var newPosition = transform.position + direction * _speed * Time.deltaTime;
        
        transform.position = newPosition;

    }
}
