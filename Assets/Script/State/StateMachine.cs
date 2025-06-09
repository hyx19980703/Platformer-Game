using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State currentState;
    public ExplosionState currentBombState;
    public void StateInitialized(State _currentState)
    {
        currentState = _currentState;
        currentState.Entry();
    }
    public void BombStateInitialized(ExplosionState _currentBombState)
    {
        currentBombState = _currentBombState;
        currentBombState.Entry();
    }


    public void StateChange(State _currentState)
    {
        currentState.Exit();
        currentState = _currentState;
        currentState.Entry();
    }
    public void BombStateChange(ExplosionState _currentBombState)
    {
        currentBombState.Exit();
        currentBombState = _currentBombState;
        currentBombState.Entry();
    }

}
