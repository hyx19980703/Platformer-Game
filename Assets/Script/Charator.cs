using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Charator : MonoBehaviour, Ideath
{
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject explosionEffect;
    public StateMachine stateMachine;

    #region 参数
    [Header("moveInfo")]

    // private bool isGround;



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
    [SerializeField] private float BombCountDown;
    public float holdBombTimer = 0f; // 新增变量，用于记录持炸弹的时间
    private bool BombCoolDown = false;


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

    private float ReturnTime = 1f;
    private float ReturnTimer;

    public CharactorMovement movement;
    void Awake()
    {

        stateMachine = new StateMachine();
        IdleState = new CharactorIdleState(this, "Idle");
        runState = new CharactorRun(this, "Run");
        airState = new CharactorAirState(this, "Air");
        deathState = new CharactorDeathState(this, "Die");
        respwanState = new CharactorRespwanState(this, "Respawn");
        throwState = new CharactorThrowState(this, "isThrowed");
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
        holdBombTimer = 0f;

    }


    void Update()
    {
        //CoolDownTimer -= Time.deltaTime;
        ReturnTimer -= Time.deltaTime;
        BombCountDown = 3f;
        stateMachine.currentState.Update();

        if ( Input.GetKey(KeyCode.Q)&& !BombCoolDown )
        {
            anim.SetLayerWeight(1, 1);
            holdBombTimer += Time.deltaTime;
            if (holdBombTimer >= BombCountDown) // 持炸弹时间达到三秒，触发爆炸
            {
                ExplodeBomb();  //只触发爆炸特效
                holdBombTimer = 0f;  
                BombCoolDown = true;
                anim.SetLayerWeight(1, 0);
                Invoke("ExplosionCoolDown", 1f);
            }
        }
        //  Vector2 mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if ( Input.GetKeyUp(KeyCode.Q)&& !BombCoolDown )//
        {
            //holdBombTimer = 0f;
            anim.SetLayerWeight(1, 0);
            stateMachine.StateChange(throwState);
            ThrownBoom(MousePositon.instance.mousePos);
            holdBombTimer = 0f;
            //CoolDownTimer = boomCoolDownDuration;
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
    public bool isGround => Physics2D.Raycast(GoundDeteced.position, Vector2.down, groundDistance, whatIsGround);  // 地面检测

    private void ThrownBoom(Vector2 _mousePositon)
    {
        GameObject boom = PrefabList.prefabList.getInstance();
        boom.transform.position = transform.position;
        Debug.Log("炸弹发射位置" + transform.position);
        Rigidbody2D boomRb = boom.GetComponent<Rigidbody2D>();

        //Explode explode = boom.GetComponent<Explode>();

        // 直接给炸弹初速度，不在这里检测地面
        Vector2 thrownDir = (_mousePositon - rb.position).normalized;
        boomRb.velocity = thrownDir * thrownSpeed;

        Explode bombScript = boom.GetComponent<Explode>();
        if (bombScript != null)
        {
            bombScript.SetHoldTime(holdBombTimer);
        }

        if ((_mousePositon.x > rb.position.x) && !isFacingRight)
        {
            facingDir = facingDir * -1;
            transform.Rotate(0, 180, 0);
            isFacingRight = true;
        }
        else if ((_mousePositon.x < rb.position.x) && isFacingRight)
        {
            facingDir = facingDir * -1;
            transform.Rotate(0, 180, 0);
            isFacingRight = false;
        }
    }

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


    public void RespwanPlayer()  // 
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
    void ReturnExplosionToPool()
    {
        if (explosionEffect != null)
        {
            ExplosionPoolManager.instance.ReturnExplosion(explosionEffect);
        }
    }
    private void ExplodeBomb() // 新增函数，用于触发炸弹爆炸
    {
        explosionEffect = ExplosionPoolManager.instance.GetExplosion();
        if (explosionEffect != null)
        {
            Vector3 offset = new Vector3(0, 1.0f, 0);
            explosionEffect.transform.position = transform.position + offset;
            explosionEffect.SetActive(true);

            Animator animator = explosionEffect.GetComponent<Animator>();
            
            if (animator != null)
            {
                animator.Play("boom400ppppp");

                // 计算动画时长并在结束时回收特效
                float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;

                Invoke("ReturnExplosionToPool", animationLength);
            }
        }
        //BombCountDown = 3f;
        holdBombTimer = 0f;
    }
    private void ExplosionCoolDown()
    {
        BombCoolDown= false;
    }
}
