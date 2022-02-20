using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralManager : MonoBehaviour
{
    public BoxCollider2D spawnArea;
    public ushort maxAmountMinerals;

    public int curCount;
    public Transform mineralPrefab;

    private Bounds bounds;

    private void Start()
    {
        bounds = this.spawnArea.bounds;
    }   

    private void SpawnMinerals()
    {
        for (int i = curCount; i < maxAmountMinerals; i++)
        {
            Transform mineral = Instantiate(this.mineralPrefab);
            
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            mineral.position = new Vector3(x, y, 0.0f);

            curCount++;
        }
    }

    private void Update()
    {
        SpawnMinerals();
    }
}
