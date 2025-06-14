using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // 定义按键事件类型（参数为按键名）
    public static event System.Action OnKeyPressed;
    
    // 触发事件的方法
    public static void TriggerKeyPressedEvent()
    {
        OnKeyPressed?.Invoke();
    }

}