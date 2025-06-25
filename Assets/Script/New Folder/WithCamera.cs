using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithCamera : MonoBehaviour
{
    private float zOffset = 5f;
    private Vector3 offsetPosition;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        offsetPosition = Camera.main.transform.position;
        offsetPosition.z = zOffset;
        transform.position = offsetPosition;
    }
}
