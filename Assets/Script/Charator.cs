using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Charator : MonoBehaviour
{
   public Rigidbody2D rb;
   public Animator anim;

   public StateMachine stateMachine;

   #region 参数
   [Header("moveInfo")]

   // private bool isGround;

   [SerializeField] private float movingSpeed;

   [SerializeField] private float jumpForce;
   [SerializeField] private int maxJumpNum = 0;

   [SerializeField] private float airMoveFactor;


   private int avaliableJump;
   private int facingDir = 1;

   [Header("collision")]
   [SerializeField] private float groundDistance;
   [SerializeField] private LayerMask whatIsGround;

   [Header("boom")]
   [SerializeField] private float thrownSpeed;

   [SerializeField] private GameObject boomPrefab;
   [SerializeField] private Vector2 thrownDir;

   #endregion
   public float xInput;
   public float yInput;

   public bool isFacingRight = true;

   #region state
   public CharactorIdleState IdleState { get; private set; }
   public CharactorRun runState { get; private set; }

   public CharactorAirState airState { get; private set; }
   #endregion

   void Awake()
   {

      stateMachine = new StateMachine();
      IdleState = new CharactorIdleState(this, "Idle");
      runState = new CharactorRun(this, "Run");
      airState = new CharactorAirState(this, "Air");
   }


   void Start()
   {
      rb = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
      stateMachine.StateInitialized(IdleState);

   }


   void Update()
   {


      stateMachine.currentState.Update();
      xInput = Input.GetAxisRaw("Horizontal");
      //  Vector2 mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);



      if (isGround)
         avaliableJump = maxJumpNum;

      if (Input.GetButtonDown("Jump") && avaliableJump > 0)
      {
         ChractorJump();
         avaliableJump--;
      }
      if (Input.GetKeyDown(KeyCode.Q))//只触发一次
         ThrownBoom(MousePositon.instance.mousePos);

      Flip();




   }
   public bool isGround => Physics2D.Raycast(transform.position, Vector2.down, groundDistance, whatIsGround);  // 地面检测



   public void ChractorMove() // 左右移动
   {
      rb.velocity = new Vector2(xInput * movingSpeed, rb.velocity.y);
   }

    public void AirMove()
    {
      rb.velocity = new Vector2(rb.velocity.x + airMoveFactor*xInput*Time.deltaTime , rb.velocity.y);
    }

    private void ChractorJump() // 跳跃
   {
      rb.velocity = new Vector2(rb.velocity.x, jumpForce);
   }

private void ThrownBoom(Vector2 _mousePositon)
{
    GameObject boom = Instantiate(boomPrefab, transform.position, Quaternion.identity);
    Rigidbody2D boomRb = boom.GetComponent<Rigidbody2D>();
    Explode explode = boom.GetComponent<Explode>();
    
    // 直接给炸弹初速度，不在这里检测地面
    Vector2 thrownDir = (_mousePositon - rb.position).normalized;
    boomRb.velocity = thrownDir * thrownSpeed;
}

   void OnDrawGizmos() // 地面检测调试
   {
      Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundDistance);
   }

   void Flip() //翻转函数
   {
      if (xInput > 0 && !isFacingRight)
      {
         facingDir = facingDir * -1;
         transform.Rotate(0, 180, 0);
         isFacingRight = true;
      }
      if (xInput < 0 && isFacingRight)
         { facingDir = facingDir * -1;
            transform.Rotate(0, 180, 0);
            isFacingRight = false;
         }
   }
}
