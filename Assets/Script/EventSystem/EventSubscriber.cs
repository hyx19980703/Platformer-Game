using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventSubscriber : MonoBehaviour
{
    public EventPublisher eventPublisher;

    void Start()
    {
        eventPublisher = FindObjectOfType<EventPublisher>();

        eventPublisher.pressDown += OnPressDownHandle;
    }


    public void OnPressDownHandle()
    {
        Debug.Log("按下down");
    }
}
