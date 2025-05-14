using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   [SerializeField] private Transform leftPoint,rightPoint;
    private Rigidbody2D rb;
    [SerializeField] private float movingSpeed;

    private bool movingRight =true;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();


  
        
           
    }

    // Update is called once per frame
    void Update()
    {

        if(movingRight)
        {
        rb.velocity = new Vector2(movingSpeed,rb.velocity.y);
        if(transform.position.x>=rightPoint.position.x)
        movingRight = false;
        }
        else {
        rb.velocity = new Vector2(-movingSpeed,rb.velocity.y);
            if(transform.position.x<=leftPoint.position.x)
            movingRight = true;
        }
    }
}
