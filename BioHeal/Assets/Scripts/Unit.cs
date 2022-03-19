using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected Rigidbody2D rb;

    protected float velocity = 2f;

    protected int force;

    protected GameObject aim = null;
    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected void Move()
    {
        Vector3 myPos = this.transform.position;

        if (aim != null)
        {
            Vector3 delta = aim.transform.position - myPos;
            delta.Normalize();

            rb.velocity = delta * velocity;
        }
        else
            rb.velocity = Vector2.zero;
    }

    public void Init(float velocity, int force)
    {
        this.velocity = velocity;
        this.force = force;
    } 
}
