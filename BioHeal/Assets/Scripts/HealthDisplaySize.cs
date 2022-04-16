using UnityEngine;

public class HealthDisplaySize : HealthDisplay
{
    private Vector2 maxScale = new Vector2(0f, 0f);

    protected override void Start()
    {
        base.Start();
        maxScale = owner.transform.localScale;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        owner.transform.localScale = new Vector2(maxScale.x * healthPercentage, maxScale.y * healthPercentage);
    }
}
