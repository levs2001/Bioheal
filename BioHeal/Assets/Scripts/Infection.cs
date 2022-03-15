using UnityEngine;

public class Infection : MonoBehaviour
{
    private Rigidbody2D rb;

    private const float velocity = 3f;

    private int force = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Heart")
        {
            SceneManager.sceneManager.DeleteObject(SceneManager.EntityType.Infection, this.gameObject);
        }
    }

    private void Move()
    {
        Vector3 infectionPosition = this.transform.position;
        Vector3? heartPosition = SceneManager.sceneManager.HeartPosition;

        if (heartPosition.HasValue)
        {
            Vector3 delta = heartPosition.Value - infectionPosition;
            delta.Normalize();

            rb.velocity = delta * velocity;
        }
    }
}
