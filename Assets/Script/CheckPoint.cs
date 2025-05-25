using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
<<<<<<< HEAD
    private bool isActive;
    [SerializeField] private int currenLevel; //该存档点对应的关卡
=======
    private bool isActive;  //激活检查点
    [SerializeField] private Sprite activePoint;
    private SpriteRenderer spriteRenderer;
>>>>>>> leveldesign

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        activePoint = GetComponentInChildren<Sprite>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive == false && collision.CompareTag("Player"))
        {
<<<<<<< HEAD
            GameManager.Instance.SetCheckPoint(transform.position,currenLevel);
=======
            GameManager.Instance.SetCheckPoint(transform.position);//设置最后检查点
>>>>>>> leveldesign
            isActive = true;
            spriteRenderer.sprite = activePoint;
            
        }
    }
}
