using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Charator : MonoBehaviour
{
   private Rigidbody2D rb;
   [SerializeField]private LayerMask whatIsGround;

   private bool isGround;
   [SerializeField]private float groundDistance;

  [SerializeField] private float movingSpeed;

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
       if(isGround)
    {
       ChractorMove();

    }

       
    }


    private void  ChractorMove()
    {
       rb.velocity = new Vector2(xInput*movingSpeed,rb.velocity.y);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,transform.position+ Vector3.down*groundDistance);
    }
}
