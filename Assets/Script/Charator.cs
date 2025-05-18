using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Charator : MonoBehaviour
{
   private Rigidbody2D rb;
#region 参数
   [Header("moveInfo")]

  // private bool isGround;

  [SerializeField] private float movingSpeed;

  [SerializeField] private float jumpForce;
   [SerializeField] private float downSpeed;
   private int maxJumpNum = 1;
   private int avaliableJump;
   private int facingDir = 1;
   
   [Header("collision")]
   [SerializeField] private float groundDistance;
   [SerializeField] private LayerMask whatIsGround;

   [Header("boom")]

   [SerializeField] private GameObject boomPrefab;
#endregion
   private float xInput;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


   void Update()
   {
         xInput = Input.GetAxisRaw("Horizontal");

      if (isGround)
      {
         ChractorMove();
      }
      else
      {
         if (xInput != 0)
         ChractorMove();
      }

      if (isGround)
         avaliableJump = maxJumpNum;

      if (Input.GetButtonDown("Jump") && avaliableJump > 0)
      {
         ChractorJump();
         avaliableJump--;
      }
if (Input.GetKeyDown(KeyCode.Q))
      ThrownBoom();

       


    }
   public bool isGround =>Physics2D.Raycast(transform.position,Vector2.down,groundDistance,whatIsGround);  // 地面检测



    private void  ChractorMove() // 左右移动
    {
       rb.velocity = new Vector2(xInput*movingSpeed,rb.velocity.y);
    }
    

    private void ChractorJump() // 跳跃
    {
       rb.velocity  = new Vector2(rb.velocity.x,jumpForce);
    }

   private void ThrownBoom() // 扔炸弹
   {
      GameObject boom = Instantiate(boomPrefab, transform.position, Quaternion.identity);
      Rigidbody2D boomRb = boom.GetComponent<Rigidbody2D>();
      boomRb.velocity = new Vector2(0, downSpeed);
   }
      
    
    void OnDrawGizmos() // 地面检测调试
    {
        Gizmos.DrawLine(transform.position,transform.position+ Vector3.down*groundDistance);
    }
}
