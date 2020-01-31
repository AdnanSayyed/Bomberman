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

    private void EndMenuState()
    {
        GameManager.Instance.AddState(new GameState());
    }
    private void Quit()
    {
        GameManager.Instance.QuitGame();
    }



}
