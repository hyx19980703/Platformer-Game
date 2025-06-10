using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class EventSubscriber : MonoBehaviour
{
    public EventPublisher eventPublisher;

    public TestEvent testEvent;

    void Start()
    {
        eventPublisher = FindObjectOfType<EventPublisher>();
        eventPublisher.pressDown += testEvent.PressEvent; //注册事件
    }

}
