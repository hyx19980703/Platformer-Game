using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaStateMachine
{
    public LavaState currentState;
    public void StateInitialized(LavaState _currentState)
    {
        currentState = _currentState;
        currentState.Entry();
    }
    public void StateChange(LavaState _currentState)
    {
        currentState.Exit();
        currentState = _currentState;
        currentState.Entry();
    }
}
