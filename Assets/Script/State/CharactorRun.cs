using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorRun : State
{
    public CharactorRun(Charator _charator, string _animName) : base(_charator, _animName)
    {
    }

    public override void Update()
    {
        base.Update();
        if (charator.isGrounded)
            Debug.Log("ÒÆ¶¯×´Ì¬");
            //charator.ChractorMove();
            charator.movement.Move(charator.xInput,charator);
        if (!charator.isGrounded)
            charator.stateMachine.StateChange(charator.airState);
        if (charator.xInput == 0)
            charator.stateMachine.StateChange(charator.IdleState);
    }

    public override void Entry()
    {
        base.Entry();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
