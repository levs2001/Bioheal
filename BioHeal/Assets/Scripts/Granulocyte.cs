using UnityEngine;

public class Granulocyte : Unit
{
    private float moveRadius = 3f;

    new private void Start()
    {
        base.Start();
        entityType = EntityType.Granulocyte;
    }

    private void FixedUpdate()
    {
        if (aim == null)
        {
            aim = SceneManager.sceneManager.GetAim(EntityType.Infection, this.transform.position);
        }
        Move();
    }

    private new void Move()
    {
        if (aim != null)
        {
            Vector3 vectorFromHeartToAim = aim.transform.position - SceneManager.sceneManager.Heart.transform.position;
            float distanseBetweenHeartAndAim = vectorFromHeartToAim.magnitude;

            if (distanseBetweenHeartAndAim > moveRadius)
            {
                float cos = vectorFromHeartToAim.x / distanseBetweenHeartAndAim;
                float sin = vectorFromHeartToAim.y / distanseBetweenHeartAndAim;
                Vector3 movePos = new Vector3(moveRadius * cos, moveRadius * sin, 0);
                movePos += SceneManager.sceneManager.Heart.transform.position;

                Vector3 delta = movePos - this.transform.position;
                if (delta.magnitude > 0.1)
                {
                    delta.Normalize();
                    rb.velocity = delta * velocity;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }
            }
            else
            {
                base.Move();
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (aim != null && other.CompareTag("Infection") && other == aim.GetComponent("Collider2D"))
        {
            Infection aimInfectionComponent = aim.GetComponent<Infection>();
            int damageTaken = aimInfectionComponent.Force;
            aimInfectionComponent.TakeDamage(force);
            this.TakeDamage(damageTaken);
        }
    }

    private void OnDestroy()
    {
        if (aim != null)
        {
            SceneManager.sceneManager.TransferEntityFromBusyToFree(EntityType.Infection, aim);
        }
    }
}
