using UnityEngine;

public class Unit : Alive
{
    protected Rigidbody2D rb;

    [SerializeField] protected float velocity = 2f;

    protected Aim aim;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    virtual protected void FixedUpdate()
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
    }

    virtual protected void FindNewAimIfNeeded()
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
        maxForce = force;
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
