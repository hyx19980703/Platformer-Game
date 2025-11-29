using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

using UnityEngine;

public class TrapTrriger : MonoBehaviour
{
    [SerializeField] float trapTrrigerDistance; //陷阱触发距离

    public float trapRunDuration ; //陷阱运行时长

    public float trapRuntimer; //陷阱运行计时器

    private Collider2D[] colliders;

    [SerializeField] float trapDistance;

  
    // Start is called before the first frame update


    private Color baseColor = Color.red;

    private float unVisuable = 0.2f;



    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
    }

    void OnDrawGizmos()
    {   
        Gizmos.color = new Color(baseColor.r,baseColor.g,baseColor.b,unVisuable);
        Gizmos.DrawSphere(transform.position,trapDistance);
    }

    public bool DetectPlayer()
    {
    colliders= Physics2D.OverlapCircleAll(transform.position,trapDistance);
    foreach(var i in colliders)
        {
            Debug.Log($"检测到{i.gameObject.tag}");
          if(i.gameObject.tag == "Player")
            {
            return  true;
            }
        } 
    return false;
}

}