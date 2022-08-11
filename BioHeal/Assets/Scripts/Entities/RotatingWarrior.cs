using UnityEngine;

//Warrior who turns for the goal
public class RotatingWarrior : Warrior
{
    override protected void Move()
    {
        base.Move();

        if (aim.entity != null)
        {
            Vector2 delta = (Vector2)aim.entity.transform.position - (Vector2)transform.position;
            var angle = Vector2.SignedAngle(transform.right, delta);
            transform.Rotate(Vector3.forward * angle);
        }
    }
}