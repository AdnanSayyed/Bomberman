using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected State currentState;

    public virtual void  Update()
    {
        if (currentState != null)
            currentState.Update();
    }

    public void AddState(State state)
    {
        currentState = state;
        currentState.OnInitialize();
    }
    public virtual void OnInputTrigger(EventState state,EventType type)
    {

    }

    public virtual void EndCurrentState()
    {

    }
}


public enum EventState
{
   startButton,
   restartButton,
   quitButton
}

public enum EventType
{
    mouseDown,
    mouseUp,
    Horizontal,
    Vertical
}