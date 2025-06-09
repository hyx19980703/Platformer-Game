using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionState : MonoBehaviour
{
    protected PrefabList explode;
    //private StateMachine stateMachine;
    protected String animName;

    public ExplosionState(PrefabList _explode, String _animName)
    {
        this.explode = _explode;
        this.animName = _animName;
    }

    public virtual void Entry()
    {
    }

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {
    }
}
