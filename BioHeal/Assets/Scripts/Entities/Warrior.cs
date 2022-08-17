using UnityEngine;

public class Warrior : Unit
{
    private void MakeDamage()
    {
        aim.entity.GetComponent<Alive>().TakeDamage(force);
    }

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if (aim.entity != null && other.CompareTag(aim.entityType.ToString()) && other == aim.entity.GetComponent("Collider2D"))
        {
            int damageTaken = aim.entity.GetComponent<Alive>().Force;
            this.MakeDamage();
            this.TakeDamage(damageTaken);
        }
    }

    private void OnDestroy()
    {
        if (aim.entity != null)
        {
            Alive aimAlive = (Alive)aim.entity.GetComponent(typeof(Alive));
            aimAlive.SubtractPotentialDamage(force);
            SceneManager.sceneManager.TransferEntityFromBusyToFree(aim.entityType.Value, aim.entity);
        }
    }
}