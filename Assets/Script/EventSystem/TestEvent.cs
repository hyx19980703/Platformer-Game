using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    public EventPublisher eventPublisher;

    void Start()
    {
        eventPublisher = FindObjectOfType<EventPublisher>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            eventPublisher.EventTrriger();
    }


    public void PressEvent()
    {
        Debug.Log("按下按键");
    }
}
