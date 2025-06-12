using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleWithBomb : State
{
    public IdleWithBomb(Charator _charator, string _animName) : base(_charator, _animName)
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
        //if (/*Input.GetKey(KeyCode.Q) &&*/ charator.movement.xInput != 0)
        //{
        //    charator.stateMachine.StateChange(charator.runWithBomb);
        //}

        //if (/*Input.GetKey(KeyCode.Q) && */!charator.isGround)
        //{
        //    charator.stateMachine.StateChange(charator.airWithBomb);
        //}


        //if (Input.GetKeyUp(KeyCode.Q))
        //{
        //    charator.stateMachine.StateChange(charator.throwState);
        //}
    }
}
