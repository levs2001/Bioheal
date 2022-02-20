using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class circleScript : MonoBehaviour
{

    public Vector2 absSpeed = new Vector2(5f, 5f);
    public Transform heart;
    public Vector2 direction; 

    // Start is called before the first frame update
    void Start()
    {
        heart = GameObject.FindGameObjectWithTag("Heart").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x > 20){
            return;
        }
        float nx = Math.Sign(heart.transform.position.x - this.transform.position.x);
        float ny = Math.Sign(heart.transform.position.y - this.transform.position.y);

        float dx = absSpeed.x * nx * Time.deltaTime;
        float dy = absSpeed.y * ny * Time.deltaTime;
        this.transform.position += new Vector3(dx, dy, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object Entered the trigger");
        if (other.gameObject.CompareTag("Heart")){
            Destroy(this.gameObject);        
        }
    }
}

