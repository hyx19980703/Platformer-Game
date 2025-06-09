using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion1 : ExplosionState 
{ 
    public Explosion1(PrefabList _explode, String _animName):base(_explode,  _animName)
    {

    }
    public override void Update()
    {
        base.Update();
        Exit();
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
