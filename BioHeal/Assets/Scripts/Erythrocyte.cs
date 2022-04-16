using UnityEngine;

public class Erythrocyte : Unit
{
    [SerializeField] private Sprite erythrocyteWithMineralSprite;
    [SerializeField] private Sprite erythrocyteSprite;

    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        entityType = EntityType.Erythrocyte;
        aim = new Aim(EntityType.Mineral);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (aim.entity != null && other.tag == "Mineral" && other == aim.entity.GetComponent("Collider2D"))
        {
            SceneManager.sceneManager.DeleteObject(EntityType.Mineral, aim.entity);
            aim.entity = SceneManager.sceneManager.Heart;
            spriteRenderer.sprite = erythrocyteWithMineralSprite;
        }

        if (aim.entity == SceneManager.sceneManager.Heart && other.tag == "Heart")
        {
            SceneManager.sceneManager.Heart.GetComponent<Base>().IncreaseMoney();
            spriteRenderer.sprite = erythrocyteSprite;
            aim.entity = null;
        }
    }

    public void ThrowAwayTheMineral()
    {
        if (aim.entity != null && aim.entity == SceneManager.sceneManager.Heart)
        {
            SceneManager.sceneManager.SpawnEntity(aim.entityType.Value, transform.position);
        }
    }
}
