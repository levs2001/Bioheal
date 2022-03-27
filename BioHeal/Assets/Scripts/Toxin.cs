using UnityEngine;

public class Toxin : Unit
{
    new private void Start()
    {
        base.Start();
        entityType = EntityType.Toxin;
        aim = new Aim(EntityType.Erythrocyte);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (aim.entity != null && other.tag == "Erythrocyte" && other == aim.entity.GetComponent("Collider2D"))
        {
            aim.entity.GetComponent<Erythrocyte>().ThrowAwayTheMineral();
            SceneManager.sceneManager.DeleteObject(EntityType.Erythrocyte, aim.entity);
            aim.entity = null;
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
