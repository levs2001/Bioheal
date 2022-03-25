using UnityEngine;
using System.Collections.Generic;

public class EntityManager
{
    private readonly List<GameObject> freeEntities;
    private readonly List<GameObject> busyEntities;

    private GameObject prefab;

    //For testing
    public List<GameObject> FreeEntities
    {
        get { return freeEntities; }
    }

    //For testing    
    public List<GameObject> BusyEntities
    {
        get { return busyEntities; }
    }

    public EntityManager(GameObject prefab)
    {
        this.prefab = prefab;
        freeEntities = new List<GameObject>();
        busyEntities = new List<GameObject>();
    }

    public GameObject GetClosestEntity(Vector3 finderPosition)
    {
        GameObject closestEntity = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject entity in freeEntities)
        {
            float dist = Vector3.Distance(finderPosition, entity.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closestEntity = entity;
            }
        }

        if (closestEntity != null)
        {
            freeEntities.Remove(closestEntity);
            busyEntities.Add(closestEntity);
        }

        return closestEntity;
    }

    public void DeleteObject(GameObject entity)
    {
        if (!busyEntities.Remove(entity))
        {
            freeEntities.Remove(entity);
        }
    }

    public void SpawnByCoordinates(Vector3 position)
    {
        GameObject entity = GameObject.Instantiate(prefab);

        entity.transform.position = position;

        freeEntities.Add(entity);
    }

    public void Spawn(BoxCollider2D spawnArea)
    {
        float x = Random.Range(spawnArea.bounds.center.x - spawnArea.size.x / 2, spawnArea.bounds.center.x + spawnArea.size.x / 2);
        float y = Random.Range(spawnArea.bounds.center.y - spawnArea.size.y / 2, spawnArea.bounds.center.y + spawnArea.size.y / 2);

        SpawnByCoordinates(new Vector3(x, y, 0.0f));
    }

}