using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EritrociteManager : MonoBehaviour
{
    public Transform eritrocitePrefab;
    private List<Transform> eritrocites;
    public ushort maxAmount;

    private Bounds bounds;

    public BoxCollider2D spawnArea;

    private void Start()
    {
        bounds = this.spawnArea.bounds;

        eritrocites = new List<Transform>();

        SpawnEritrocites();
    }

    private void SpawnEritrocites()
    {
        for (int i = 0; i < maxAmount; i++)
        {
            Transform eritrocite = Instantiate(this.eritrocitePrefab);

            float x = Random.Range(bounds.min.x, bounds.max.y);
            float y = Random.Range(bounds.min.y, bounds.max.y);

            eritrocite.position = new Vector3(x, y, 0.0f);

            eritrocites.Add(eritrocite);
        }
    }
}
