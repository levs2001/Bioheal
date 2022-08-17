using UnityEngine;

public class Toxin : RotatingWarrior
{
    protected override void Start()
    {
        base.Start();
        entityType = EntityType.Toxin;
        aim = new Aim(EntityType.Erythrocyte);
    }

    protected override void OnTriggerStay2D(Collider2D other)
    {
        base.OnTriggerStay2D(other);
        if (aim.entity != null && other.tag == aim.entityType.ToString() && other == aim.entity.GetComponent("Collider2D"))
        {
            aim.entity.GetComponent<Erythrocyte>().ThrowAwayTheMineral();
        }
    }
}
