using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrriger : MonoBehaviour
{
    public bool isClosing {get ;private set ;}

    void Start()
    {
        isClosing = false;
    }

    public void ClosingDoor()
    {
        isClosing = !isClosing;
    }
}
