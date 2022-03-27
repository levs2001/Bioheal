using UnityEngine;

public class Lymphocyte : Warrior
{
    new private void Start()
    {
        base.Start();
        entityType = EntityType.Lymfocyte;
        aim = new Aim(EntityType.Toxin);
    }

    private void OnDestroy()
    {
        if (aim.entity != null)
        {
            SceneManager.sceneManager.TransferEntityFromBusyToFree(EntityType.Toxin, aim.entity);
        }
    }
}
