using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBonus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            var player = other.GetComponent<Player>();
            player.AddMoney(10);
            Destroy(gameObject);
        }
    }
    
}
