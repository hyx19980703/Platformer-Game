using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorMovement : MonoBehaviour
{
    #region 组件获取
    public Rigidbody2D rb;
    #endregion
    #region 移动参数
    [Header("moveInfo")]
    private float movingSpeed;
    private float jumpForce;
    public int maxJumpNum;
    private float airMoveFactor;
    public int availableJump;
    #endregion
    #region 地面碰撞检测参数(暂未使用，已注释)
    //[Header("collision")]
    //public Transform GroundDeteced;
    //public float groundDistance;
    //public LayerMask whatIsGround;
    #endregion
    #region 初始化方法
    public void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        this.movingSpeed = 8f;
        this.jumpForce = 10f;
        this.maxJumpNum = 1;
        this.airMoveFactor = 10f;
        availableJump = maxJumpNum;
    }
    #endregion
    #region 移动方法
    public void Move(float xInput)
    {
        //if (IsGrounded())
        //{
            rb.velocity = new Vector2(xInput * movingSpeed, rb.velocity.y);
        //}
        //else
        //{
        //    AirMove(xInput);
        //}
    }
    #endregion
    #region 跳跃方法
    public void Jump()
    {
        //if (IsGrounded() || availableJump >= 0)
        //{
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            //if (!IsGrounded())
            //{
            //    availableJump--;
            //}
        //}
    }
    #endregion
    #region 空中移动方法
    public void AirMove(float xInput)
    {
        rb.velocity = new Vector2(rb.velocity.x + airMoveFactor * xInput * Time.deltaTime, rb.velocity.y);
    }
    #endregion
    #region 地面碰撞检测方法(暂未使用，已注释)
    //public bool IsGrounded()
    //{
    //    bool isGrounded = Physics2D.Raycast(GroundDeteced.position, Vector2.down, groundDistance, whatIsGround);
    //    if (isGrounded)
    //    {
    //        availableJump = maxJumpNum;
    //    }
    //    return isGrounded;
    //}
    #endregion
    //void Awake()
    //{
    //    GroundDeteced = GetComponent<Transform>();
    //}
}
