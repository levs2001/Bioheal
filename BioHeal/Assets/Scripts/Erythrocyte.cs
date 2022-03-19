using UnityEngine;

public class Erythrocyte : MonoBehaviour
{
    private Rigidbody2D rb;

    private const float velocity = 3f;

    private GameObject aim = null;

    [SerializeField] private Sprite erythrocyteWithMineralSprite;
    [SerializeField] private Sprite erythrocyteSprite;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

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
            spriteRenderer.sprite = erythrocyteWithMineralSprite;
        }

        if (aim == SceneManager.sceneManager.Heart && other.tag == "Heart")
        {
            SceneManager.sceneManager.Heart.GetComponent<Base>().IncreaseMoney();
            spriteRenderer.sprite = erythrocyteSprite;
            aim = null;
        }
    }

    private void OnDestroy() {
        if (aim != null && aim == SceneManager.sceneManager.Heart)
        {
            SceneManager.sceneManager.SpawnEntity(SceneManager.EntityType.Mineral, transform.position);
        }
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
}
