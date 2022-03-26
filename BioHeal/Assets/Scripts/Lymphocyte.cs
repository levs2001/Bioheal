using UnityEngine;

public class Lymphocyte : Unit
{
    private void FixedUpdate()
    {
        if (aim == null)
        {
            aim = SceneManager.sceneManager.GetAim(EntityType.Toxin, this.transform.position);
        }
        Move();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (aim != null && other.CompareTag("Toxin") && other == aim.GetComponent("Collider2D"))
        {
            Toxin aimInfectionComponent = aim.GetComponent<Toxin>();
            int damageTaken = aimInfectionComponent.Force;
            aimInfectionComponent.TakeDamage(force);
            this.TakeDamage(damageTaken);
        }
    }

    private void OnDestroy() {
        if (aim != null)
        {
            SceneManager.sceneManager.TransferEntityFromBusyToFree(EntityType.Toxin, aim);
        }
    }
}
