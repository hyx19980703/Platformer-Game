using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    public State currentState;
    public void StateInitialized(State _currentState)
    {
        currentState = _currentState;
        currentState.Entry();
    }

    public void StateChange(State _currentState)
    {
        currentState.Exit();
        currentState = _currentState;
        currentState.Entry();
    }
}
