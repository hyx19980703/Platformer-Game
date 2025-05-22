using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool isActive;  //激活检查点
    [SerializeField] private Sprite activePoint;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        activePoint = GetComponentInChildren<Sprite>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive == false && collision.CompareTag("Player"))
        {
            GameManager.Instance.SetCheckPoint(transform.position);//设置最后检查点
            isActive = true;
            spriteRenderer.sprite = activePoint;
            
        }
    }
}
