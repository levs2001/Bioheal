using UnityEngine;

public class Alive : MonoBehaviour
{
    [SerializeField] protected int force;
    [SerializeField] protected int maxForce;

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

        if (force <= 0)
        {
            Destroy(healthbar);
            SceneManager.sceneManager.DeleteObject(entityType, this.gameObject);
        }

        if (entityTakeDamageEvent != null)
        {
            entityTakeDamageEvent();
        }
    }

    protected virtual void Start()
    {
        healthbar = Instantiate(GameObject.FindWithTag(SceneManager.sceneManager.healthDisplayTypeTag), transform.position, Quaternion.identity);
        healthbar.GetComponent<HealthDisplay>().Owner = this;
    }
}

