using UnityEngine;

public class Toxin : MonoBehaviour
{
    private Rigidbody2D rb;

    private const float velocity = 2f;

    private int force;

    private GameObject aim = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

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

    private void Move()
    {
        Vector3 toxinPos = this.transform.position;

        if (aim != null)
        {
            Vector3 delta = aim.transform.position - toxinPos;
            delta.Normalize();

            rb.velocity = delta * velocity;
        }
        else
            rb.velocity = Vector2.zero;
    }
}
