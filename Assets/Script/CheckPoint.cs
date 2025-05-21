using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool isActive;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive == false && collision.CompareTag("Player"))
        {
            GameManager.Instance.SetCheckPoint(transform.position);
            isActive = true;
        }
    }
}
