using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class GameState : State
{
    private event SubState OnSubState;

    public override void OnInitialize()
    {
        GameManager.Instance.StartGame();
    }
    public override void Update()
    {
        OnSubState?.Invoke();
        
    }

}
