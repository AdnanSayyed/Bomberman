public class State
{
    protected delegate void SubState();

    public virtual void OnInitialize(){ }

    public virtual void OnStateStart() { }

    public virtual void Update() { }

    public virtual void OnStateEnd() { }

    public virtual void  OnNextState() { }

    public virtual void OnInputTrigger(EventState state,EventType type) { }
}
