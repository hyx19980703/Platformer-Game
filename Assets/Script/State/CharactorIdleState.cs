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
        Debug.Log("闲置状态!");
        if (charator.isGround && charator.xInput != 0)
        {
            Debug.Log("转换状态");
            charator.stateMachine.StateChange(charator.runState);
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
