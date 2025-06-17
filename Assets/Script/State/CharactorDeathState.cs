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
        Debug.Log("进入死亡状态");
        charator.movement.rb.velocity = Vector3.zero;//清除玩家速度
        charator.movement.rb.simulated = false; //禁用物理模拟
    }

    public override void Exit()
    {
        base.Exit();
        GameObject.FindWithTag("Player").transform.position = GameManager.Instance.lastPosition; //玩家恢复正常位置
        Debug.Log("回复活点");
        charator.movement.rb.simulated = true; //恢复物理模拟 
    }

    public override void Update()
    {
        base.Update();
        Input.ResetInputAxes(); //禁用输入，效果等同于玩家无法移动
    }
}
