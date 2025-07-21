using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorThrowState : State
{
    // Start is called before the first frame update
    public CharactorThrowState(Charator _charator, string _animName):base(_charator, _animName)
    {
    }

    public override void Update()
    {
        base.Update();
        //charator.stateMachine.StateChange(charator.IdleState);
    }
    public override void Entry()
    {
        base.Entry();
        //Debug.Log("扔炸弹");
    }

    public override void Exit()
    {
        base.Exit();
    }
}
