using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    [Range(0, 1f)]
    public float eruptionProbability;

    public Animator lavaAnim;
    public LavaStateMachine lavaStateMachine;
    public LavaBurstState lavaBurstState { get; private set; }
    public LavaIdleState lavaIdleState { get; private set; }
    public BoxCollider2D boxCollider2D;

    private int currentFrame;
    void Start()
    {
        lavaAnim = GetComponent<Animator>();
        lavaStateMachine = new LavaStateMachine();
        boxCollider2D = GetComponent<BoxCollider2D>();

        lavaBurstState = new LavaBurstState(this,"LavaBurst");
        lavaIdleState = new LavaIdleState(this, "LavaIdle");

        lavaStateMachine.StateInitialized(lavaIdleState);
    }

    // Update is called once per frame
    void Update()
    {
        lavaStateMachine.currentState.Update();
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.ExplosionHurtEvent();
        }
    }

    private void ChangeToIdleState()
    {
        lavaStateMachine.StateChange(lavaIdleState);
    }
}
