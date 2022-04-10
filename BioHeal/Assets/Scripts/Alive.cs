using UnityEngine;

public class Alive : MonoBehaviour
{
    [SerializeField] protected int force;
    [SerializeField] protected int maxForce;

    protected EntityType entityType;
    protected delegate void EntityTakeDamage();
    protected event EntityTakeDamage entityTakeDamageEvent = null;
    protected GameObject healthbar;

    protected Vector2 maxScale = new Vector2(0f,0f);

    public int Force
    {
        set { force = value; }
        get { return force; }
    }

    public void TakeDamage(int damage)
    {
        force -= damage;
        if (healthbar != null && Loader.LoaderInstance.healthDisplayType == HealthDisplayType.BAR)
        {
            healthbar.GetComponent<HealthBar>().Force -= damage;
        }

        // TODO: refactor this part
        if (Loader.LoaderInstance.healthDisplayType == HealthDisplayType.MODEL_SIZE)
        {
            float scaleFactor = force * 1.0f / maxForce;
            this.transform.localScale = new Vector2(maxScale.x * scaleFactor, maxScale.y * scaleFactor);    
        }

        if (force <= 0)
        {
            SceneManager.sceneManager.DeleteObject(entityType, this.gameObject);
        }
        if (entityTakeDamageEvent != null)
            entityTakeDamageEvent();
    }
}

