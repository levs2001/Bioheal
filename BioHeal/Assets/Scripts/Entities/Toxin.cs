using UnityEngine;

public class Toxin : Warrior
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

    private void OnDestroy()
    {
        if (aim.entity != null)
        {
            SceneManager.sceneManager.TransferEntityFromBusyToFree(EntityType.Erythrocyte, aim.entity);
        }
    }
}
