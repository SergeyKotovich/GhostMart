using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBonus : MonoBehaviour
{
    public static event Action<int> MoneyAdd; // все не правильно =) нужно убрать static
    private void OnTriggerEnter(Collider other)
    {
        MoneyAdd?.Invoke(10);
        Destroy(gameObject);
    }

    public void AddMoney()
    {
        
    }
}
