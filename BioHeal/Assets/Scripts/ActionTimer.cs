public class ActionTimer
{
    private float elapsedTime = 0f;

    private float timerResetTime;

    public delegate void Action();
    private Action someAction;

    public ActionTimer(float timerResetTime, Action action)
    {
        this.timerResetTime = timerResetTime;
        someAction += action;
    }

    public void AddTimeAndDoAction(float deltaTime) {
        elapsedTime += deltaTime;
        if (elapsedTime >= timerResetTime)
        {
            elapsedTime = 0;
            if (someAction != null) someAction();
        }
    }
}