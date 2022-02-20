using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class circleManagerScript : MonoBehaviour
{

    public Transform circlePrefab;
    public Bounds spawnBounds;
    public BoxCollider2D spawnArea;
    public List<Transform> circles;
    public int initialNumberCircles = 1;
    public float deltaSpawn = 5f;

    // Start is called before the first frame update
    void Start()
    {
        this.circles = new List<Transform>();
        this.spawnBounds = this.spawnArea.bounds;
        InvokeRepeating("spawnCircle", 0f, deltaSpawn);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawnCircle() {
        
        Transform circle = Instantiate(this.circlePrefab);
        
        float x = Random.Range(spawnBounds.min.x, spawnBounds.max.x);
        float y = Random.Range(spawnBounds.min.y, spawnBounds.max.y);
        circle.position = new Vector2(x,y);
        circles.Add(circle);

    }

}

