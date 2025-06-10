using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
    protected  Charator charator;
    //private StateMachine stateMachine;
    protected  String animName;

    public State(Charator _charator, String _animName)
    {
        this.charator = _charator;
        this.animName = _animName;
    }

    public virtual void Entry()
    {
        charator.anim.SetBool(animName, true);
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public virtual void Exit()
    {
       charator.anim.SetBool(animName, false);
    }
}
