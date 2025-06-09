using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPublisher : MonoBehaviour
{
    public System.Action pressDown;

    public void OnpressDown()
    {
        pressDown?.Invoke();  //若事件不为空，则触发事件。
    }
    
}