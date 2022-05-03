using UnityEngine;

public abstract class HealthDisplay : MonoBehaviour
{
    private int maxForce = 100;
    private int force = 50;
    protected float healthPercentage = 0;
    // TODO: Think about using only transorm from owner
    protected Alive owner;

    public Alive Owner
    {
        set { owner = value; }
    }

    protected virtual void Start()
    {
        maxForce = owner.Force;
        force = owner.Force;
    }

    protected virtual void FixedUpdate()
    {
        force = owner.Force;
        healthPercentage = Mathf.Min(Mathf.Max(0, force * 1f / maxForce), 1);
    }
}
