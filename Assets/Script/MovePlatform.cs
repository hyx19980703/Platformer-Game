using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class MovePlatform : MonoBehaviour
{
     [SerializeField] private Transform[] movePoint;
     [SerializeField] private float moveSpeed ;
     [SerializeField] private Transform originPoint;

     private int currentMovePoint;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

       transform.position = Vector2.MoveTowards(transform.position,movePoint[currentMovePoint].position,Time.deltaTime);    
       Debug.Log("distance"+Vector2.Distance(transform.position,movePoint[currentMovePoint].position)+"currentMovePoint"+currentMovePoint);
              
               if(Vector2.Distance(transform.position,movePoint[currentMovePoint].position)<0.1 )
               {
                  currentMovePoint = (currentMovePoint + 1)%movePoint.Length;
               }



                
        }
    }

