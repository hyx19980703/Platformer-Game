using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirWithBomb : State
{
    public AirWithBomb(Charator _charator, string _animName) : base(_charator, _animName)
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
        charator.movement.Move();
        charator.anim.SetFloat("yVector", charator.movement.rb.velocity.y);

        if (charator.isGround)
        {
            charator.stateMachine.StateChange(charator.idleWithBomb);
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            charator.stateMachine.StateChange(charator.throwState);
        }
    }
}