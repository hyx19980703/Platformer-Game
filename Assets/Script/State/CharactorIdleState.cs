using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class CharactorIdleState : State
{
    public CharactorIdleState(Charator _charator, string _animName) : base(_charator, _animName)
    {
    }

    public override void Update()
    {
        base.Update();
        if (charator.isGround && charator.movement.xInput != 0)
        {
            charator.stateMachine.StateChange(charator.runState);
            //ChractorMove();
        }

                if (!charator.isGround)
        {
            charator.stateMachine.StateChange(charator.airState);
            //ChractorMove();
        }
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
