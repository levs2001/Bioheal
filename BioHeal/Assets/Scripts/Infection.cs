using UnityEngine;

public class Infection : Unit
{
    private new void Start()
    {
        base.Start();
        base.aim = SceneManager.sceneManager.Heart;
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Heart")
        {
            SceneManager.sceneManager.DeleteObject(EntityType.Infection, this.gameObject);
        }
    }
}
