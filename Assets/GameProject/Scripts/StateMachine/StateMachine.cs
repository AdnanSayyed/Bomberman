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

    public virtual void QuitGame()
    {
        if (currentState != null)
        {
            currentState = null;
        }
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}


public enum EventState
{
   startButton,
   restartButton,
   quitButton,
   movementButton
}

public enum EventType
{
    mouseDown,
    mouseUp,
    Horizontal,
    Vertical
}