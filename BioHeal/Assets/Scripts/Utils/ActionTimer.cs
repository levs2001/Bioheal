using UnityEngine;

public class ActionTimer : MonoBehaviour
{
    private float elapsedTime = 0f;

    private float timerResetTime;

    public delegate void Action();
    private Action someAction;

    public Action SomeAction
    {
        set { someAction += value; }
    }

    public float Timer
    {
        set { timerResetTime = value; }
    }

    public void FixedUpdate() {
        elapsedTime += Time.fixedDeltaTime;
        if (elapsedTime >= timerResetTime)
        {
            elapsedTime = 0;
            if (someAction != null) someAction();
        }
    }
}