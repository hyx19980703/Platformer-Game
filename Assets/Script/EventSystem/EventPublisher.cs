using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPublisher : MonoBehaviour
{
    public System.Action pressDown; //定义事件

    public void EventTrriger() // 事件触发方法
    {
        pressDown?.Invoke();  //若事件不为空，则触发事件。
    }


}