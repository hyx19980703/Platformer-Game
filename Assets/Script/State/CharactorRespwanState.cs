using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorRespwanState : State
{
    public CharactorRespwanState(Charator _charator, string _animName) : base(_charator, _animName)
    {

    }

    public override void Entry()
    {
        base.Entry();
        //GameObject.FindWithTag("Player").transform.position = GameManager.Instance.lastPosition; //玩家恢复正常位置
        charator.anim.SetLayerWeight(1, 0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        Input.ResetInputAxes(); //禁用输入，效果等同于玩家无法移动
    }
}
