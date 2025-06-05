using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Charator : MonoBehaviour, Ideath
{
    //public Rigidbody2D rb;
   public Animator anim;
   public CharactorMovement movement;
   public StateMachine stateMachine;

   #region 参数
   //[Header("moveInfo")]

   //// private bool isGround;

   //[SerializeField] private float movingSpeed;

   //[SerializeField] private float jumpForce;
   //[SerializeField] private int maxJumpNum = 0;

   //[SerializeField] private float airMoveFactor;


   //private int avaliableJump;
   private int facingDir = 1;

    [Header("collision")]
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask whatIsGround;

    [Header("boom")]
    [SerializeField] private float thrownSpeed;

    [SerializeField] private GameObject boomPrefab;
    [SerializeField] private Vector2 thrownDir;

    [SerializeField] private float boomCoolDownDuration;
    [SerializeField] private float CoolDownTimer;
    [SerializeField] private Transform GroundDeteced;

   #endregion
   public float xInput;
   public float yInput;

   public bool isFacingRight = true;

   #region state
   public CharactorIdleState IdleState { get; private set; }
   public CharactorRun runState { get; private set; }

   public CharactorAirState airState { get; private set; }

   public CharactorDeathState deathState { get; private set; }

   public CharactorRespwanState respwanState { get; private set; }
   #endregion

   private float ReturnTime = 1f;
   private float ReturnTimer;
   void Awake()
   {

      stateMachine = new StateMachine();
      IdleState = new CharactorIdleState(this, "Idle");
      runState = new CharactorRun(this, "Run");
      airState = new CharactorAirState(this, "Air");
      deathState = new CharactorDeathState(this, "Die");
      respwanState = new CharactorRespwanState(this, "Respawn");

      movement = gameObject.AddComponent<CharactorMovement>();
      

    }


    void Start()
   {
      
      anim = GetComponentInChildren<Animator>();
      stateMachine.StateInitialized(IdleState);
      movement.Initialize();


    }


   void Update()
   {
      CoolDownTimer -= Time.deltaTime;
      ReturnTimer -= Time.deltaTime;

      stateMachine.currentState.Update();
      xInput = Input.GetAxisRaw("Horizontal");
        movement.Move(xInput);
        //  Vector2 mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (isGrounded /*&& Input.GetAxisRaw("Horizontal") != 0*/)
        {
            movement.Move(xInput);
        }

        if (!isGrounded /*&& Input.GetAxisRaw("Horizontal") != 0*/)
        {
            movement.AirMove(xInput);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)  
        {
            movement.Jump();
            movement.availableJump --;
        }

        if (!isGrounded)
        {
            movement.availableJump = 0;
        }

        //if (xInput != 0 && movement.IsGrounded())
        //  {
        //      stateMachine.StateChange(runState);
        //  }
        //else if (xInput == 0 && movement.IsGrounded() )
        //  {
        //      stateMachine.StateChange(IdleState);
        //  }
        if (Input.GetKeyDown(KeyCode.Q) && CoolDownTimer < 0)//
      {
         ThrownBoom(MousePositon.instance.mousePos);
         CoolDownTimer = boomCoolDownDuration;
      }

      Flip();
       /*如果不加时间，重生函数会因为玩家一直处于死亡线的位置而重复调用，转变状态时启用回复位置函数，最终玩家在死亡动画播放完成前就回到起始点 
       还有一种做法是把回复位置函数放在respwan状态的entry函数
       */
      if (isUnderDeathLine() && ReturnTimer < 0)
      {
         ReturnTimer = ReturnTime;
         RespwanPlayer();
      }

   }
    public bool isGrounded => Physics2D.Raycast(GroundDeteced.position, Vector2.down, groundDistance, whatIsGround);  // 地面检测



    //public void ChractorMove() // 左右移动
    //{
    //   rb.velocity = new Vector2(xInput * movingSpeed, rb.velocity.y);
    //}

    //public void AirMove()
    //{
    //   rb.velocity = new Vector2(rb.velocity.x + airMoveFactor * xInput * Time.deltaTime, rb.velocity.y);
    //}

    //private void ChractorJump() // 跳跃
    //{
    //   rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    //}

    private void ThrownBoom(Vector2 _mousePositon)
   {
      GameObject boom = PrefabList.prefabList.getInstance();
      boom.transform.position = transform.position;
      Debug.Log("炸弹发射位置" + transform.position);
      Rigidbody2D boomRb = boom.GetComponent<Rigidbody2D>();

      //Explode explode = boom.GetComponent<Explode>();

      // 直接给炸弹初速度，不在这里检测地面
      Vector2 thrownDir = (_mousePositon - movement.rb.position).normalized;
      boomRb.velocity = thrownDir * thrownSpeed;
   }

   //void OnDrawGizmos() // 地面检测调试
   //{
   //   Gizmos.DrawLine(GroundDeteced.position, GroundDeteced.position + Vector3.down * groundDistance);
   //}

   void Flip() //翻转函数
   {
      if (xInput > 0 && !isFacingRight)
      {
         facingDir = facingDir * -1;
         transform.Rotate(0, 180, 0);
         isFacingRight = true;
      }
      if (xInput < 0 && isFacingRight)
      {
         facingDir = facingDir * -1;
         transform.Rotate(0, 180, 0);
         isFacingRight = false;
      }
   }


   public  void RespwanPlayer()  // 
    { 
  
        this.stateMachine.StateChange(this.deathState); //进入死亡状态

           
    }
    


  public bool isUnderDeathLine()
   {
      if (this.transform.position.y <= GameManager.Instance.deathLine.position.y)  
         return true;
      else
         return false;
   }
}
