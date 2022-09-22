using UnityEngine;
using System.Collections.Generic;
// using System;

public class EntityManager
{
    protected readonly List<GameObject> freeEntities;
    private readonly List<GameObject> busyEntities;

    protected GameObject prefab;
    private SpawnAreas spawnAreas;

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

    public EntityManager(GameObject prefab, EntityType entityType)
    {
        this.prefab = prefab;
        freeEntities = new List<GameObject>();
        busyEntities = new List<GameObject>();

        spawnAreas = new SpawnAreas(entityType);
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

        return closestEntity;
    }

    public void DeleteObject(GameObject entity)
    {
        if (!busyEntities.Remove(entity))
        {
            freeEntities.Remove(entity);
        }
    }

    public void TransferFromBusyToFree(GameObject entity)
    {
        if (busyEntities.Remove(entity))
        {
            freeEntities.Add(entity);
        }
    }

    public void TransferFromFreeToBusy(GameObject entity)
    {
        if (freeEntities.Remove(entity))
        {
            busyEntities.Add(entity);
        }
    }

    public virtual void SpawnByCoordinates(Vector3 position)
    {
        GameObject entity = GameObject.Instantiate(prefab);
        entity.transform.position = position;
        freeEntities.Add(entity);
    }

    public void Spawn()
    {
        BoxCollider2D spawnArea = spawnAreas.randomEntitySpawnArea;

        float x = Random.Range(spawnArea.bounds.center.x - spawnArea.size.x / 2, spawnArea.bounds.center.x + spawnArea.size.x / 2);
        float y = Random.Range(spawnArea.bounds.center.y - spawnArea.size.y / 2, spawnArea.bounds.center.y + spawnArea.size.y / 2);

        SpawnByCoordinates(new Vector3(x, y, 0.0f));
    }
}