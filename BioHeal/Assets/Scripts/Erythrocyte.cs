using UnityEngine;

public class Erythrocyte : MonoBehaviour
{
    private Rigidbody2D rb;

    private float velocity = 3f;

    private GameObject aim = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Move()
    {
        Vector3 erythrocytePos = transform.position;

        if (aim != null)
        {
            Vector3 delta = aim.transform.position - erythrocytePos;
            delta.Normalize();

            rb.velocity = delta * velocity;
        }
        else
            rb.velocity = Vector2.zero;
    }

    private void Update()
    {
        if (aim == null)
            aim = SceneManager.sceneManager.GetAim(SceneManager.EntityType.Mineral, this.transform.position);
    }

    private void FixedUpdate()
    {
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
