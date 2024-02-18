using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBonus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    public void AddMoney()
    {
        
    }
}
