using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroghPlatform : MonoBehaviour
{
    [SerializeField] private PlatformEffector2D effector;
    //private LayerMask onewayPlatform;

    private float passingDuration = 0.5f;

    private float passingTimer;

   // private Collider collider;

    private bool isPassingThrogh;
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        EventManager.OnKeyPressed += PassingThrough;
        
    }
    /*
    简易方案 platformeffector unity自带的单向平台组件，可以通过角度和范围角来判断可穿越区域 
    rotationloffset=0为从正下方穿越
     ratationloffest=180为从正上方穿越
    */
    void Update()
    {
        passingTimer -= Time.deltaTime;
            if(passingTimer<0)
           effector.rotationalOffset = 0;
    }

    public void PassingThrough()
    {
        Debug.Log("触发单向功能");
        effector.rotationalOffset = 180;
        passingTimer = passingDuration;

    }

    // 备用方案 层级碰撞
    // public IEnumerator PassingThrough()
    // {
    //     if (isPassingThrogh == true) yield break;
    //     isPassingThrogh = true;
    //     Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("OneWayPlatform"), true);
    //     Debug.Log("禁用层级碰撞");
    //     yield return new WaitForSeconds(passingDuration);
    //     Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("OneWayPlatform"), false);
    //     Debug.Log("恢复层级碰撞");
    //     isPassingThrogh = false;
        

    // }




}
