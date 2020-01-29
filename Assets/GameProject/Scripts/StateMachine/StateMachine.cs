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

public abstract class State
{
    protected delegate void SubState();

    public virtual void OnInitialize()
    {

    }

    public virtual void OnStateStart() { }

    public virtual void Update() { }

    public virtual void OnStateEnd() { }

    private void OnNextState()
    {

    }

    public virtual void OnInputTrigger(EventState state,EventType type)
    {
        switch (state)
        {
            case EventState.startButton:
                switch (type)
                {
                    case EventType.mouseDown:
                        break;
                    case EventType.mouseUp:
                        break;
                }
                break;
            case EventState.restartButton:
                break;
            case EventState.quitButton:
                switch (type)
                {
                    case EventType.mouseDown:
                        break;
                    case EventType.mouseUp:
                        break;
                }
                break;
            default:
                break;
        }
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