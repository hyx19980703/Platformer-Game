using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class RunWithBomb : State
{
    public RunWithBomb(Charator _charator, string _animName) : base(_charator, _animName)
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
        //charator.movement.Move();
        //if (/*Input.GetKey(KeyCode.Q) &&*/ charator.movement.xInput == 0)
        //{
        //    charator.stateMachine.StateChange(charator.idleWithBomb);
        //}

        ////if (Input.GetKeyUp(KeyCode.Q))
        ////{
        ////    charator.stateMachine.StateChange(charator.throwState);
        ////}
    }
}
