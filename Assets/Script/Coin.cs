using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
   private int valueCoin = 10;


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Charator>())
        {
            GameManager.Instance.AddSource(valueCoin);
            Destroy(gameObject);
        }
    }
    void EatCoin()//test method
    {
        Console.WriteLine("12345678");
    }
   
}
