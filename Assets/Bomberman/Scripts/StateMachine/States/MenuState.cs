using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

/// <summary>
/// Menu state 
/// Initial game state
/// </summary>
public class MenuState : State
{
    private event SubState OnSubState;

    public override void OnInitialize()
    {
       
    }
    public override void Update()
    {
        OnSubState?.Invoke();
    }

    /// <summary>
    /// sets substate on button clicks
    /// </summary>
    /// <param name="state">button name</param>
    /// <param name="type">button state</param>
    public override void OnInputTrigger(EventState state, EventType type)
    {
        switch (state)
        {
            case EventState.startButton:
                switch (type)
                {
                    case EventType.mouseDown:
                        OnSubState = EndMenuState;
                        break;
                }
                break;
            case EventState.quitButton:
                switch (type)
                {
                    case EventType.mouseDown:
                        OnSubState = Quit;
                        break;
                }
                break;
        }

    }

    /// <summary>
    /// Substae which ends current state and sets next state
    /// </summary>
    private void EndMenuState()
    {
        GameManager.Instance.AddState(new GameState());
    }

    /// <summary>
    /// Substate for quit game
    /// </summary>
    private void Quit()
    {
        GameManager.Instance.QuitGame();
    }



}
