using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBonus : MonoBehaviour
{
    [SerializeField] private Player _player;
    private void OnTriggerEnter(Collider other)
    {
        _player.AddMoney(10);
        Destroy(gameObject);
    }
    
}
