using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorMovement : MonoBehaviour
{
    #region 组件获取
    public Rigidbody2D rb;
    private IDetection groundDetected;
    private Charator charatorIsGounded;
    #endregion
    #region 移动参数
    [Header("moveInfo")]
    [SerializeField] private float movingSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] public int maxJumpNum;
    [SerializeField] private float airMoveFactor;
    [HideInInspector] public int availableJump;
    public float xInput;
    #endregion
    #region 初始化方法(暂未使用，已注释)
    //public void Initialize()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    this.movingSpeed = 8f;
    //    this.jumpForce = 10f;
    //    this.maxJumpNum = 1;
    //    this.airMoveFactor = 10f;
    //    availableJump = maxJumpNum;
    //}
    #endregion
    #region 移动方法
    public void Move()
    {
        if (charatorIsGounded.isGround)
        {
            rb.velocity = new Vector2(xInput * movingSpeed, rb.velocity.y);
        }
        if (!charatorIsGounded.isGround)
        {
            AirMove(xInput);
        }
    }
    #endregion
    #region 跳跃方法
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        SoundManager.instance.PlaySound("jump");
    }
    #endregion
    #region 空中移动方法
    public void AirMove(float xInput)
    {
        rb.velocity = new Vector2(rb.velocity.x + airMoveFactor * xInput * Time.deltaTime, rb.velocity.y);
    }
    #endregion
    void Awake()
    {
        charatorIsGounded = GetComponent<Charator>();
        rb = GetComponent<Rigidbody2D>();
        groundDetected = GetComponent<GroundD>();
    }
    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if(charatorIsGounded.isGround)
        {
            availableJump = maxJumpNum;
        }
        else
        {
            availableJump = 0;
        }

        if (Input.GetButtonDown("Jump")&& availableJump > 0 )
        {
            Jump();
            availableJump--;
        }
    }
}
