using UnityEngine;
using System.Collections.Generic;
using System;

public class SceneManager : MonoBehaviour
{
    private const string PathToErythrocytePrefab = "Entities/Erythrocyte";
    private const string PathToGranulocytePrefab = "Entities/Granulocyte";
    private const string PathToHeartPrefab = "Entities/Heart";
    private const string PathToInfectionPrefab = "Entities/Infection";
    private const string PathToLymfocytePrefab = "Entities/Lymfocyte";
    private const string PathToMineralPrefab = "Entities/Mineral";
    private const string PathToToxinPrefab = "Entities/Toxin";

    private const string PathToBaseSpawnPoint = "SpawnAreas/BaseSpawnPoint/SpawnPoint";

    public enum EntityType
    {
        Infection,
        Erythrocyte,
        Toxin,
        Lymfocyte,
        Granulocit,
        Mineral
    }

    public static SceneManager sceneManager { get; private set; }

    private Dictionary<EntityType, EntityManager> entityManagers;

    private Dictionary<EntityType, float> spawnFrequencies;
    private Dictionary<EntityType, float> elapsedTimeSinceLastSpawn;

    private SpawnAreas spawnAreas;

    private GameObject heart;

    public GameObject Heart
    {
        get
        {
            return heart;
        }
    }

    public Vector3? HeartPosition
    {
        get
        {
            if (heart != null)
                return heart.transform.position;
            else
                return null;
        }
    }

    public void SpawnEntity(EntityType entityType, Vector3? position = null)
    {
        EntityManager entityManager = entityManagers[entityType];
        BoxCollider2D spawnArea = null;

        switch (entityType)
        {
            case EntityType.Infection:
            case EntityType.Toxin:
                spawnArea = spawnAreas.GetRandomArea(SpawnAreas.EntityClass.enemy);
                break;

            case EntityType.Erythrocyte:
            case EntityType.Lymfocyte:
            case EntityType.Granulocit:
                spawnArea = spawnAreas.GetRandomArea(SpawnAreas.EntityClass.allied);
                break;

            case EntityType.Mineral:
                spawnArea = spawnAreas.GetRandomArea(SpawnAreas.EntityClass.mineral);
                break;

            default:
                break;
        }

        if (!position.HasValue)
            entityManager.Spawn(spawnArea);
        else
            entityManager.SpawnByCoordinates(position.Value);
    }

    public GameObject GetAim(EntityType aimType, Vector3 finderPosition)
    {
        EntityManager entityManager = entityManagers[aimType];
        return entityManager.GetClosestEntity(finderPosition);
    }

    public void DeleteObject(EntityType entityType, GameObject objectToDelete)
    {
        EntityManager entityManager = entityManagers[entityType];
        entityManager.DeleteObject(objectToDelete);
        Destroy(objectToDelete);
    }

    private Dictionary<EntityType, GameObject> SetPrefabs()
    {
        Dictionary<EntityType, GameObject> prefabs = new Dictionary<EntityType, GameObject>();

        prefabs[EntityType.Infection] = Resources.Load<GameObject>(PathToInfectionPrefab);
        prefabs[EntityType.Erythrocyte] = Resources.Load<GameObject>(PathToErythrocytePrefab);
        prefabs[EntityType.Lymfocyte] = Resources.Load<GameObject>(PathToLymfocytePrefab);
        prefabs[EntityType.Granulocit] = Resources.Load<GameObject>(PathToGranulocytePrefab);
        prefabs[EntityType.Toxin] = Resources.Load<GameObject>(PathToToxinPrefab);
        prefabs[EntityType.Mineral] = Resources.Load<GameObject>(PathToMineralPrefab);

        return prefabs;
    }

    private void SetFrequencies()
    {
        spawnFrequencies = new Dictionary<EntityType, float>();

        spawnFrequencies[EntityType.Infection] = 3;
        spawnFrequencies[EntityType.Erythrocyte] = 3;
        spawnFrequencies[EntityType.Lymfocyte] = Mathf.Infinity;
        spawnFrequencies[EntityType.Granulocit] = Mathf.Infinity;
        spawnFrequencies[EntityType.Toxin] = 19;
        spawnFrequencies[EntityType.Mineral] = 0.5f;

        elapsedTimeSinceLastSpawn = new Dictionary<EntityType, float>();
        foreach (EntityType type in Enum.GetValues(typeof(EntityType)))
        {
            elapsedTimeSinceLastSpawn[type] = 0;
        }
    }

    private void Awake()
    {
        sceneManager = this;

        entityManagers = new Dictionary<EntityType, EntityManager>();

        spawnAreas = new SpawnAreas();

        SetFrequencies();

        Dictionary<EntityType, GameObject> prefabs = SetPrefabs();
        foreach (EntityType type in Enum.GetValues(typeof(EntityType)))
        {
            entityManagers[type] = new EntityManager(prefabs[type]);
        }
    }

    private void Start()
    {
        GameObject heartPrefab = Resources.Load<GameObject>(PathToHeartPrefab);
        heart = GameObject.Instantiate(heartPrefab);
        GameObject baseSpawnPoint = Resources.Load<GameObject>(PathToBaseSpawnPoint);
        heart.transform.position = baseSpawnPoint.transform.position;
    }

    private void Update()
    {
        foreach (EntityType type in Enum.GetValues(typeof(EntityType)))
        {
            if (elapsedTimeSinceLastSpawn[type] >= spawnFrequencies[type])
            {
                SpawnEntity(type);
                elapsedTimeSinceLastSpawn[type] = 0;
            }
            elapsedTimeSinceLastSpawn[type] += Time.deltaTime;
        }
    }
}
