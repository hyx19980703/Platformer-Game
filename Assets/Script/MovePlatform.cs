using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class MovePlatform : MonoBehaviour
{
     [SerializeField] private Transform[] movePoint;
     [SerializeField] private float moveSpeed ;

     private int currentMovePoint;
    void Start()
    {
    }


    void Update()
    {

       transform.position = Vector2.MoveTowards(transform.position,movePoint[currentMovePoint].position,Time.deltaTime);    
      
              
               if(Vector2.Distance(transform.position,movePoint[currentMovePoint].position)<0.1 )
               {
                  currentMovePoint = (currentMovePoint + 1)%movePoint.Length;   //等于自身长度时余数为0，不会超数组
               }



                
        }
    }

