using UnityEngine;

public class Toxin : Unit
{
    private void FixedUpdate() {
        if (aim == null)
            aim = SceneManager.sceneManager.GetAim(SceneManager.EntityType.Erythrocyte, this.transform.position);
        Move();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (aim != null && other.tag == "Erythrocyte" && other == aim.GetComponent("Collider2D"))
        {
            SceneManager.sceneManager.DeleteObject(SceneManager.EntityType.Erythrocyte, aim);
            aim = null;
        }
    }
}
