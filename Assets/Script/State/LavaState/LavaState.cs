using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaState
{
    public LavaController lavaController;

    private string lavaAnimName;

    public LavaState (LavaController _lavaController, String _lavaAnimName)
    {
        this.lavaController = _lavaController;
        this.lavaAnimName = _lavaAnimName;
    }

    public virtual void Entry()
    {
        lavaController.lavaAnim.SetBool(lavaAnimName, true);
    }

    public virtual void Update ()
    {

    }

    public virtual void Exit()
    {
        lavaController.lavaAnim.SetBool(lavaAnimName, false);
    }
}
