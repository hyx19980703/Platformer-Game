using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaIdleState : LavaState
{
    public LavaIdleState(LavaController _LavaController,string _LavaAnimName) : base(_LavaController,_LavaAnimName)
    {

    }
    public override void Entry()
    {
        base.Entry();
        EventManager.OnExplosion += StateChange;
        Debug.Log("进入初始状态");
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

    private void StateChange()
    {
        lavaController.lavaStateMachine.StateChange(lavaController.lavaBurstState);
    }
}
