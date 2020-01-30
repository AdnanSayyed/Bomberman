public class State
{
    protected delegate void SubState();

    public virtual void OnInitialize()
    {

    }

    public virtual void OnStateStart() { }

    public virtual void Update() { }

    public virtual void OnStateEnd() { }

    private void OnNextState()
    {

    }

    public virtual void OnInputTrigger(EventState state,EventType type)
    {
        switch (state)
        {
            case EventState.startButton:
                switch (type)
                {
                    case EventType.mouseDown:
                        break;
                    case EventType.mouseUp:
                        break;
                }
                break;
            case EventState.restartButton:
                break;
            case EventState.quitButton:
                switch (type)
                {
                    case EventType.mouseDown:
                        break;
                    case EventType.mouseUp:
                        break;
                }
                break;
            default:
                break;
        }
    }
}
