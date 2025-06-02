using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroghPlatform : MonoBehaviour
{
    [SerializeField]private GameObject charator;
    //private LayerMask onewayPlatform;

    private float passingDuration = 0.5f;

   // private Collider collider;

    private bool isPassingThrogh;
    void Start()
    {
        charator = GameObject.FindWithTag("Player");
        Debug.Log("玩家层级 " + charator.layer);
        Debug.Log("穿越平台层级 " + LayerMask.NameToLayer("OneWayPlatform"));
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            StartCoroutine("PassingThrough");
    }

    IEnumerator PassingThrough()
    {
        if (isPassingThrogh == true) yield break;
        isPassingThrogh = true;
        Physics2D.IgnoreLayerCollision(charator.layer, LayerMask.NameToLayer("OneWayPlatform"), true);
        Debug.Log("禁用层级碰撞");
        yield return new WaitForSeconds(passingDuration);
        Physics2D.IgnoreLayerCollision(charator.layer, LayerMask.NameToLayer("OneWayPlatform"), false);
        Debug.Log("恢复层级碰撞");
        isPassingThrogh = false;
        

    }




}
