using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Charator : MonoBehaviour
{
   private Rigidbody2D rb;
   [SerializeField]private LayerMask whatIsGround;

   private bool isGround;
   [SerializeField]private float groundDistance;

  [SerializeField] private float movingSpeed;

  [SerializeField] private float jumpForce;

   private int maxJumpNum =1;

   private int avaliableJump;

   private int facingDir = 1;

   private float  xInput;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {

     isGround =Physics2D.Raycast(transform.position,Vector2.down,groundDistance,whatIsGround);
        
       xInput = Input.GetAxisRaw("Horizontal");
       ChractorMove();

       if(isGround)
       avaliableJump = maxJumpNum;

       if(Input.GetButtonDown("Jump")&&avaliableJump>0)
       {
       ChractorJump();
        avaliableJump--;
       }

       


    }


    private void  ChractorMove()
    {
       rb.velocity = new Vector2(xInput*movingSpeed,rb.velocity.y);
    }
    

    private void ChractorJump()
    {
       rb.velocity  = new Vector2(rb.velocity.x,jumpForce);
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,transform.position+ Vector3.down*groundDistance);
    }
}
