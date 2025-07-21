using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBurstState : LavaState
{
    public LavaBurstState(LavaController _lavaController,string _lavaAnimName) : base(_lavaController,_lavaAnimName)
    {

    }
    public override void Entry()
    {
        base.Entry();
        EventManager.OnExplosion += StateChange;
    }
    public override void Update()
    {
        base.Update();
    }
    public override void Exit()
    {
        base.Exit();
        EventManager.OnExplosion -= StateChange;

    }
    public void StateChange()
    {

    }
}
