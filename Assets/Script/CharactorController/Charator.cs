using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
//using UnityEditor.Scripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.TextCore.Text;

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
   public int facingDir = 1;

    [Header("collision")]
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask whatIsGround;

    
    [SerializeField] private Transform GoundDeteced;

    #endregion

    public bool isFacingRight = true;
    public MousePositon mousePositon;

    #region state
    public CharactorIdleState IdleState { get; private set; }
    public CharactorRun runState { get; private set; }

    public CharactorAirState airState { get; private set; }

    public CharactorDeathState deathState { get; private set; }

    public CharactorRespwanState respwanState { get; private set; }

    public CharactorThrowState throwState { get; private set; }

    public IdleWithBomb idleWithBomb { get; private set; }

    public RunWithBomb runWithBomb { get; private set; }

    public AirWithBomb airWithBomb { get; private set; }
    #endregion

    public float ReturnTime = 1f;
    public float ReturnTimer;

    public CharactorMovement movement;
    void Awake()
    {
        stateMachine = new StateMachine();
        IdleState = new CharactorIdleState(this, "Idle");
        runState = new CharactorRun(this, "Run");
        airState = new CharactorAirState(this, "Air");
        deathState = new CharactorDeathState(this, "Die");
        respwanState = new CharactorRespwanState(this, "Respawn");
        throwState = new CharactorThrowState(this, "IsThrowed");
        //idleWithBomb = new IdleWithBomb(this, "IdleWithBomb");
        //runWithBomb = new RunWithBomb(this, "RunWithBomb");
        //airWithBomb = new AirWithBomb(this, "AirWithBomb");
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        stateMachine.StateInitialized(IdleState);
        movement = GetComponent<CharactorMovement>();
    }


    void Update()
    {
        ReturnTimer -= Time.deltaTime;
        stateMachine.currentState.Update();
        Flip();
        //Debug.Log(Camera.main.targetTexture?.name);
    }
    public bool isGround => Physics2D.Raycast(GoundDeteced.position, Vector2.down, groundDistance, whatIsGround);  // 地面检测
    void OnDrawGizmos() // 地面检测调试
    {
        Gizmos.DrawLine(GoundDeteced.position, GoundDeteced.position + Vector3.down * groundDistance);
    }

    void Flip() //翻转函数
    {

        if (movement.xInput > 0 && !isFacingRight)
        {
            facingDir = facingDir * -1;
            transform.Rotate(0, 180, 0);
            isFacingRight = true;
        }
        if (movement.xInput < 0 && isFacingRight)
        {
            facingDir = facingDir * -1;
            transform.Rotate(0, 180, 0);
            isFacingRight = false;
        }
    }
}
