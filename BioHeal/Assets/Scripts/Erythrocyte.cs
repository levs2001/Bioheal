using UnityEngine;

public class Erythrocyte : Unit
{
    [SerializeField] private Sprite erythrocyteWithMineralSprite;
    [SerializeField] private Sprite erythrocyteSprite;

    private SpriteRenderer spriteRenderer;

    new private void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (aim == null)
            aim = SceneManager.sceneManager.GetAim(EntityType.Mineral, this.transform.position);
        Move();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (aim != null && other.tag == "Mineral" && other == aim.GetComponent("Collider2D"))
        {
            SceneManager.sceneManager.DeleteObject(EntityType.Mineral, aim);
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

    private void OnDestroy()
    {
        if (aim != null && aim == SceneManager.sceneManager.Heart)
        {
            SceneManager.sceneManager.SpawnEntity(EntityType.Mineral, transform.position);
        }
    }
}
