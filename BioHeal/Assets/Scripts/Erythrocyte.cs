using UnityEngine;

public class Erythrocyte : Unit
{
    private void FixedUpdate()
    {
        if (aim == null)
            aim = SceneManager.sceneManager.GetAim(SceneManager.EntityType.Mineral, this.transform.position);
        Move();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (aim != null && other.tag == "Mineral" && other == aim.GetComponent("Collider2D"))
        {
            SceneManager.sceneManager.DeleteObject(SceneManager.EntityType.Mineral, aim);
            aim = SceneManager.sceneManager.Heart;
        }

        if (aim == SceneManager.sceneManager.Heart && other.tag == "Heart")
        {
            aim = null;
        }
    }

    private void OnDestroy() {
        if (aim != null && aim == SceneManager.sceneManager.Heart)
        {
            SceneManager.sceneManager.SpawnEntity(SceneManager.EntityType.Mineral, transform.position);
        }
    }
}
