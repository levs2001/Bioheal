using UnityEngine;

public class Granulocyte : Warrior 
{
    private float moveRadius = 3f;

    new private void Start()
    {
        base.Start();
        entityType = EntityType.Granulocyte;
        aim = new Aim(EntityType.Infection);
    } 

    new private void FixedUpdate() {
        
        FindNewAimIfNeeded();
        Move();
    }

    new private void Move()
    {
        if (aim.entity != null)
        {
            Vector3 vectorFromHeartToAim = aim.entity.transform.position - SceneManager.sceneManager.Heart.transform.position;
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
    
    private void OnDestroy()
    {
        if (aim.entity != null)
        {
            SceneManager.sceneManager.TransferEntityFromBusyToFree(aim.entityType.Value, aim.entity);
        }
    }
}
