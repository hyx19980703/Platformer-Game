using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class EventSubscriber : MonoBehaviour
{
    public EventPublisher eventPublisher;

    public ThroghPlatform throghPlatform ;

    void Start()
    {   
        eventPublisher = FindObjectOfType<EventPublisher>();
        eventPublisher.pressDown += throghPlatform.PassingThrough; //注册按下蹲按键
    }

}
