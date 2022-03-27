using UnityEngine;

public class Infection : Unit
{
    private new void Start()
    {
        base.Start();
        entityType = EntityType.Infection;
        aim = new Aim(null, SceneManager.sceneManager.Heart);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (aim.entity != null && other.CompareTag("Heart") && other == aim.entity.GetComponent("Collider2D"))
        {
            Base aimHeartComponent = aim.entity.GetComponent<Base>();
            int damageTaken = aimHeartComponent.Force;
            aimHeartComponent.TakeDamage(force);
            this.TakeDamage(damageTaken);
        }
    }
}
