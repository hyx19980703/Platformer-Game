using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundD : MonoBehaviour,IDetection
{
    [SerializeField] private Transform GroundDeteced;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask whatIsGround;

    public bool IsGrounded()
    {
        return Physics2D.Raycast(GroundDeteced.position, Vector2.down, groundDistance, whatIsGround);
    }

}
