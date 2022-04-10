using UnityEngine;
using UnityEngine.UI;

public class Unit : Alive
{
    protected Rigidbody2D rb;

    [SerializeField] protected float velocity = 2f;

    protected Aim aim;
    private RectTransform healthbarPos;

    public GameObject HealthDisplay 
    {
        set 
        {
            healthbar = value;
            
            if (Loader.LoaderInstance.healthDisplayType == HealthDisplayType.BAR)
            {
                HealthBar hb = healthbar.GetComponent<HealthBar>();
                hb.Force = force;
                hb.MaxForce = force;

                healthbarPos = healthbar.GetComponent<RectTransform>();
                healthbarPos.sizeDelta = new Vector2 (0.8f, 0.25f);
            }
        }
    }

    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        this.maxScale = this.transform.localScale;
    }

    protected void FixedUpdate()
    {
        FindNewAimIfNeeded();
        Move();
    }

    virtual protected void Move()
    {
        Vector2 myPos = this.transform.position;

        if (aim.entity != null)
        {
            Vector2 delta = (Vector2)aim.entity.transform.position - myPos;
            delta.Normalize();

            rb.velocity = delta * velocity;
        }
        else
            rb.velocity = Vector2.zero;
        
        if (healthbarPos != null)
        {
            healthbarPos.anchoredPosition = new Vector3(myPos.x, myPos.y + 0.5f, 0);
        }
    }

    protected void FindNewAimIfNeeded()
    {
        if (aim.entity == null)
        {
            aim.entity = SceneManager.sceneManager.GetAim(aim.entityType.Value, this.transform.position);
        }
    }

    public void Init(float velocity, int force)
    {
        this.velocity = velocity;
        this.force = force;
        this.maxForce = force;
    }

    protected struct Aim
    {
        public readonly EntityType? entityType;

        public GameObject entity;

        public Aim(EntityType? entityType, GameObject entity = null)
        {
            this.entityType = entityType;
            this.entity = entity;
        }
    }
}
