using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorAirState : State
{
    public CharactorAirState(Charator _charator, string _animName) : base(_charator, _animName)
    {

    }

    public override void Entry()
    {
        base.Entry();
    }
    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();
        Debug.Log("空中状态");
        charator.AirMove();

        if (charator.isGround)
        charator.stateMachine.StateChange(charator.IdleState);
    }
}
