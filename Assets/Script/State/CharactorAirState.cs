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
        //charator.AirMove();
        charator.movement.AirMove(charator.yInput);
        charator.anim.SetFloat("yVector", charator.movement.rb.velocity.y);

        if (charator.isGrounded)
            charator.stateMachine.StateChange(charator.IdleState);
    }
}
