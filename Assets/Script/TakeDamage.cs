using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Charator>())
            GameManager.Instance.HealthLess();
    }
}
