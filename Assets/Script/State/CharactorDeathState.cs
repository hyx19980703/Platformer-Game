using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorDeathState : State

{
    public CharactorDeathState(Charator _charator, string _animName) : base(_charator, _animName)
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
     //   charator.rb.velocity = new Vector2(0, 0);
    }
}
