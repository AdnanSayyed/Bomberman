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

    public void GenerateComponents()
    {
        GameManager.Instance.GenerateComponents();
        OnSubState = GenerateLevel;
    }
    public void GenerateLevel()
    {
        GameManager.Instance.levelManager.GenerateLevel();

        OnSubState = GamePlay;
    }

    public void GamePlay()
    {
        if (GameManager.Instance.playerPrefab.PlayerDied)
            OnSubState = GameOver;
    }

    public void GameOver()
    {

    }

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
    
    public void Quit()
    {
        GameManager.Instance.QuitGame();
    }

    public void RestartGame()
    {
        OnSubState = null;
        GameManager.Instance.RestartGame();
        OnSubState = GamePlay;
    }
    


}
