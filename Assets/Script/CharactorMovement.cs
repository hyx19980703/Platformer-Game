using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorMovement : MonoBehaviour
{
    #region �����ȡ
    public Rigidbody2D rb;
    #endregion
    #region �ƶ�����
    [Header("moveInfo")]
    private float movingSpeed;
    private float jumpForce;
    public int maxJumpNum;
    private float airMoveFactor;
    public int availableJump;
    #endregion
    #region ������ײ������(��δʹ�ã���ע��)
    //[Header("collision")]
    //public Transform GroundDeteced;
    //public float groundDistance;
    //public LayerMask whatIsGround;
    #endregion
    #region ��ʼ������
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
    #region �ƶ�����
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
    #region ��Ծ����
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
    #region �����ƶ�����
    public void AirMove(float xInput)
    {
        rb.velocity = new Vector2(rb.velocity.x + airMoveFactor * xInput * Time.deltaTime, rb.velocity.y);
    }
    #endregion
    #region ������ײ��ⷽ��(��δʹ�ã���ע��)
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
