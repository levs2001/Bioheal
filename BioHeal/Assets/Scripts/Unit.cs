using UnityEngine;

public class Unit : Alive
{
    protected Rigidbody2D rb;

    [SerializeField] protected float velocity = 2f;

    protected Aim aim;
    private RectTransform healthbarPos;

    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        
        // healthbarPos.anchoredPosition = new Vector3(myPos.x, myPos.y, 0);
    }

    protected void FindNewAimIfNeeded()
    {
        if (aim.entity == null)
            aim.entity = SceneManager.sceneManager.GetAim(aim.entityType.Value, this.transform.position);
    }

    public void Init(float velocity, int force, GameObject healthbarPrefab)
    {
        this.velocity = velocity;
        this.force = force;
        // this.healthbar = GameObject.Instantiate(healthbarPrefab);
        // this.healthbarPos = this.healthbar.GetComponent<RectTransform>();
        // healthbar.GetComponent<RectTransform>().sizeDelta = new Vector2 (10, 10);
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
