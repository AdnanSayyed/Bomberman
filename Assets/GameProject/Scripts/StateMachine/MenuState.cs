using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


}
