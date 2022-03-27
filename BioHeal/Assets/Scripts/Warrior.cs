using UnityEngine;

public class Warrior : Unit
{
    private void MakeDamage()
    {
        aim.entity.GetComponent<Unit>().TakeDamage(force);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (aim.entity != null && other.CompareTag(aim.entityType.ToString()) && other == aim.entity.GetComponent("Collider2D"))
        {
            
            int damageTaken = aim.entity.GetComponent<Unit>().Force;
            this.MakeDamage();
            this.TakeDamage(damageTaken);
        }
    }
}