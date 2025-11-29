using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class DoorTrape : MonoBehaviour
{
   [SerializeField] private BoxCollider2D collider2D;    
    [SerializeField] private Vector2 colliderSizeMultiplier = Vector2.one; // 快速创建 vector2(1,1)   // 用于在Inspector中调整碰撞机大小的乘数
    [SerializeField] private Vector2 colliderOffsetMultiplier = Vector2.one; // 快速创建 vector2(1,1) // 用于在Inspector中调整碰撞机偏移的乘数
     private Animator anim;

    private Vector2 originalColliderSize;     // 存储碰撞机的初始大小
    private Vector2 originalColliderOffset;     // 存储碰撞机的初始偏移
    private int pressPositionID;

    private float pressPosition;

    [SerializeField] TrapTrriger trapTrriger;

    [SerializeField] float delayTime; //延迟启动时间

    [SerializeField] float duration; //持续时间
    
    private bool isTrriger;


  

    void Start()
    {

        
        collider2D = GetComponent<BoxCollider2D>();
        anim = GetComponentInChildren<Animator>();

        originalColliderSize = collider2D.size;
        originalColliderOffset = collider2D.offset;
        
         pressPositionID = Animator.StringToHash("pressPosition"); //获取变量名的哈希值，用户后续调用时提高性能
    }

    void Update() {

        pressPosition = anim.GetFloat(pressPositionID);  // 同 GetFloat("pressPosition") ,但性能更高
        DoorColliderHight(pressPosition);
        


        if(trapTrriger.DetectPlayer()) //检测玩家
        { 
          //  StartCoroutine(DelayTrap(delayTime)); //延迟一定时间启动
            StartCoroutine(TrapTimeControl(delayTime,duration));
        }


        
    }

    private void DoorColliderHight(float _pressPosition)
    {
        collider2D.size = new Vector2(originalColliderSize.x * colliderOffsetMultiplier.x,
        originalColliderSize.y+colliderSizeMultiplier.y*_pressPosition);

        collider2D.offset = new Vector2(originalColliderOffset.x,
        originalColliderOffset.y-_pressPosition/2);

    }

    IEnumerator TrapTimeControl(float _delayTime,float _duration)  //启动动画机
    {
        Debug.Log($"等待{_delayTime}秒,执行开启动画机");
        yield return new WaitForSeconds(_delayTime);
            anim.SetBool("isTrriger",true); //启动动画机
        yield return new WaitForSeconds(_duration);
            anim.SetBool("isTrriger",false); //终止动画
        
    }


}
