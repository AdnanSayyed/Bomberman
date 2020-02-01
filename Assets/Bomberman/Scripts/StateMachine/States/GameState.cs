using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

/// <summary>
/// In Game state of game
/// Initialize after Menu state
/// </summary>
public class GameState : State
{

    private event SubState OnSubState;

    public override void OnInitialize()
    {
        OnSubState = InitializeGameState;
    }

    public override void Update()
    {
        OnSubState?.Invoke();
    }

    public void InitializeGameState()
    {
        OnSubState = GenerateComponents;
    }

    /// <summary>
    /// Substate generated required components
    /// </summary>
    public void GenerateComponents()
    {
        GameManager.Instance.CreateComponents();
        OnSubState = GenerateLevel;
    }

    /// <summary>
    /// Substate generated level blocks
    /// </summary>
    public void GenerateLevel()
    {
        GameManager.Instance.levelManager.GenerateLevel();
        OnSubState = GamePlay;
    }

    /// <summary>
    /// Substate runs until player died
    /// </summary>
    public void GamePlay()
    {
        if (GameManager.Instance.playerPrefab.PlayerDied)
            OnSubState = GameOver;
    }


    /// <summary>
    /// Substate on game over
    /// </summary>
    public void GameOver()
    {

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
                break;
            case EventState.restartButton:
                switch (type)
                {
                    case EventType.mouseDown:
                        if(OnSubState==GameOver)
                            OnSubState = RestartGame;
                        break;
                }
                break;
            case EventState.quitButton:
                switch (type)
                {
                    case EventType.mouseDown:
                        if (OnSubState == GameOver)
                            OnSubState = Quit;
                        break;
                }
                break;
        }
    }
    
    /// <summary>
    /// Substate to quit
    /// </summary>
    public void Quit()
    {
        GameManager.Instance.QuitGame();
    }

    /// <summary>
    /// Substate for restart game
    /// </summary>
    public void RestartGame()
    {
        OnSubState = null;
        GameManager.Instance.RestartGame();
        OnSubState = GamePlay;
    }
    


}
