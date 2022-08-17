using UnityEngine;

public class Alive : MonoBehaviour
{
    [SerializeField] protected int force;

    protected int potentialDamageTaken = 0;

    protected EntityType entityType;
    protected delegate void EntityTakeDamage();
    protected event EntityTakeDamage entityTakeDamageEvent = null;
    protected GameObject healthbar;

    public int Force
    {
        set { force = value; }
        get { return force; }
    }

    public int PotentialDamage
    {
        get {return potentialDamageTaken; }
    }

    public void AddPotentialDamage(int damage)
    {
        potentialDamageTaken += damage;
    }

    public void SubtractPotentialDamage(int damage)
    {
        potentialDamageTaken -= damage;
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
        healthbar = Instantiate(SceneManager.sceneManager.healthbarPrefab, transform.position, Quaternion.identity);
        healthbar.GetComponent<HealthDisplay>().Owner = this;
    }
}

