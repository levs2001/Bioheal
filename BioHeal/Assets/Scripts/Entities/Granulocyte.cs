using UnityEngine;

public class Granulocyte : Warrior 
{
    private float moveRadius = 3f;

    protected override void Start()
    {
        base.Start();
        entityType = EntityType.Granulocyte;
        aim = new Aim(EntityType.Infection);
    } 

    protected override void FixedUpdate() {
        
        FindNewAimIfNeeded();
        Move();
    }

    protected override void Move()
    {
        if (aim.entity != null)
        {
            Vector2 vectorFromHeartToAim = aim.entity.transform.position - SceneManager.sceneManager.Heart.transform.position;
            float distanseBetweenHeartAndAim = vectorFromHeartToAim.magnitude;

            if (distanseBetweenHeartAndAim > moveRadius)
            {
                float cos = vectorFromHeartToAim.x / distanseBetweenHeartAndAim;
                float sin = vectorFromHeartToAim.y / distanseBetweenHeartAndAim;
                Vector2 movePos = new Vector2(moveRadius * cos, moveRadius * sin);
                movePos += (Vector2)SceneManager.sceneManager.Heart.transform.position;

                Vector2 delta = movePos - (Vector2)this.transform.position;
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
