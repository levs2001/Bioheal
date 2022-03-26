using UnityEngine;

public class Toxin : Unit
{
    new private void Start()
    {
        base.Start();
        entityType = EntityType.Toxin;
    }

    private void FixedUpdate()
    {
        if (aim == null)
            aim = SceneManager.sceneManager.GetAim(EntityType.Erythrocyte, this.transform.position);
        Move();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (aim != null && other.tag == "Erythrocyte" && other == aim.GetComponent("Collider2D"))
        {
            SceneManager.sceneManager.DeleteObject(EntityType.Erythrocyte, aim);
            aim = null;
        }
    }

    private void OnDestroy()
    {
        if (aim != null)
        {
            SceneManager.sceneManager.TransferEntityFromBusyToFree(EntityType.Erythrocyte, aim);
        }
    }
}
