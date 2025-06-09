using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    public EventSubscriber eventSubscriber;
    // Start is called before the first frame update
    void Start()
    {
        eventSubscriber = FindObjectOfType<EventSubscriber>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
        eventSubscriber.OnPressDownHandle();
        }
    }
}
