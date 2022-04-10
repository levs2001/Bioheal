using UnityEngine;
using System.Collections.Generic;
// using System;

public class EntityManager
{
    private readonly List<GameObject> freeEntities;
    private readonly List<GameObject> busyEntities;

    private GameObject prefab;
    private bool hasForce = false;
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
        this.hasForce = entityType != EntityType.Mineral;
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

    public void TransferFromBusyToFree(GameObject entity)
    {
        if (busyEntities.Remove(entity))
        {
            freeEntities.Add(entity);
        }
    }

    public void SpawnByCoordinates(Vector3 position)
    {
        GameObject entity = GameObject.Instantiate(prefab);

        entity.transform.position = position;

        freeEntities.Add(entity);
        if (hasForce)
        {
            SetHealthDisplay(Loader.LoaderInstance.healthDisplayType);
        }
    }

    public void Spawn()
    {
        BoxCollider2D spawnArea = spawnAreas.randomEntitySpawnArea;

        float x = Random.Range(spawnArea.bounds.center.x - spawnArea.size.x / 2, spawnArea.bounds.center.x + spawnArea.size.x / 2);
        float y = Random.Range(spawnArea.bounds.center.y - spawnArea.size.y / 2, spawnArea.bounds.center.y + spawnArea.size.y / 2);

        SpawnByCoordinates(new Vector3(x, y, 0.0f));
    }

    private void SetHealthDisplay(HealthDisplayType healthDisplay)
    {

        switch (healthDisplay)
        {
            case HealthDisplayType.BAR:
                SetBarDisplay();
                break;
            case HealthDisplayType.NONE:
                break;
            case HealthDisplayType.MODEL_SIZE:
                break;
            default: throw new System.Exception("not handled health display type");
        }
    }

    private void SetBarDisplay()
    {
        GameObject entity = freeEntities[freeEntities.Count - 1];
        GameObject healthbar = GameObject.Instantiate(SceneManager.sceneManager.healthbarPrefab, entity.transform.position, Quaternion.identity);
        entity.GetComponent<Unit>().HealthDisplay = healthbar;
    }

}