using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

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
        OnSubState = GenerateManagers;
    }

    public void GenerateManagers()
    {
        GameManager.Instance.GenerateManagers();
        OnSubState = GenerateLevel;
    }
    public void GenerateLevel()
    {
        GameManager.Instance.levelManager.GenerateLevel();

        OnSubState = GamePlay;
    }

    public void GamePlay()
    {
        if (GameManager.Instance.playerDied)
            OnSubState = GameOver;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
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
                        if (OnSubState == GameOver)
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
