using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    private bool isActive;
    [SerializeField] private int currenLevel; //该存档点对应的关卡

    [SerializeField] private Sprite activePoint;
    private SpriteRenderer spriteRenderer;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive == false && collision.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound("get_checkpoint");

            GameManager.Instance.SetCheckPoint(transform.position, currenLevel);

            isActive = true;
            spriteRenderer.sprite = activePoint;
            SaveSystem.SaveGame(currenLevel, collision.transform.position);
            
        }
    }
}
