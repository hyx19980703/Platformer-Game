using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapHammer : MonoBehaviour
{



    // Start is called before the first frame update

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("玩家被攻击");
    }
}
