using UnityEngine;

public class Alive : MonoBehaviour
{
    [SerializeField] protected int force;

    protected EntityType entityType;

    protected delegate void EntityTakeDamage();
    protected event EntityTakeDamage entityTakeDamageEvent = null;
    protected GameObject healthbar;
    public int Force
    {
        set { force = value; }
        get { return force; }
    }

    public void TakeDamage(int damage)
    {
        force -= damage;
        healthbar.GetComponent<HealthBar>().Force -= damage;
        if (force <= 0)
        {
            SceneManager.sceneManager.DeleteObject(entityType, this.gameObject);
        }
        if (entityTakeDamageEvent != null)
            entityTakeDamageEvent();
    }
}

