using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

/// <summary>
/// State machine for game 
/// </summary>
public class StateMachine : MonoBehaviour
{
    protected State currentState;

    public virtual void  Update()
    {
        if (currentState != null)
            currentState.Update();
    }

    /// <summary>
    /// Add the state which replace current state
    /// </summary>
    /// <param name="state">state to add</param>
    public void AddState(State state)
    {
        currentState = state;
        currentState.OnInitialize();
    }

    
    public virtual void OnInputTrigger(EventState state,EventType type) {}

    public virtual void EndCurrentState(){}

    /// <summary>
    /// Quits the game
    /// </summary>
    public virtual void QuitGame()
    {
        if (currentState != null)
        {
            currentState = null;
        }
        if (GameManager.Instance != null)
        {
            GameManager.Instance = null;
        }
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}