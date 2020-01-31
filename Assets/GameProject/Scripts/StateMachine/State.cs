using System.Collections;

public class State
{
    protected delegate void SubState();

    public delegate void CallBack();
    private event CallBack OnCallBack;

    public virtual void OnInitialize(){ }

    public virtual void OnStateStart() { }

    public virtual void Update() { }

    public virtual void OnStateEnd() { }

    public virtual void  OnNextState() { }

    public virtual void OnInputTrigger(EventState state,EventType type) { }

}
