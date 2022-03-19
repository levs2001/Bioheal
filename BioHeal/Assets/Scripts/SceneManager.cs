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


    public static SceneManager sceneManager { get; private set; }

    private Dictionary<EntityType, EntityManager> entityManagers;

    private Dictionary<EntityType, float> spawnFrequencies;
    private Dictionary<EntityType, float> elapsedTimeSinceLastSpawn;
    private Dictionary<EntityType, GameObject> prefabs;

    private SpawnAreas spawnAreas;

    private GameObject heart;

    //Используется для установки цели для эритроцита
    public GameObject Heart
    {
        get
        {
            return heart;
        }
    }

    //Используется для того, чтобы инфекция знала позицию базы
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

    private void Awake()
    {
        sceneManager = this;

        entityManagers = new Dictionary<EntityType, EntityManager>();

        spawnAreas = new SpawnAreas();

        SetFrequencies();

        prefabs = SetPrefabs();

        heart = GameObject.FindWithTag("Heart");
        InitFromDB();
        foreach (EntityType type in Enum.GetValues(typeof(EntityType)))
        {
            entityManagers[type] = new EntityManager(prefabs[type]);
        }

    }

    private void Update()
    {
        List<EntityType> spawnEntityTypes = new List<EntityType>(elapsedTimeSinceLastSpawn.Keys);

        foreach (EntityType type in spawnEntityTypes)
        {
            if (elapsedTimeSinceLastSpawn[type] >= spawnFrequencies[type])
            {
                SpawnEntity(type);
                elapsedTimeSinceLastSpawn[type] = 0;
            }
            elapsedTimeSinceLastSpawn[type] += Time.deltaTime;
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
            case EntityType.Granulocyte:
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
        prefabs[EntityType.Granulocyte] = Resources.Load<GameObject>(PathToGranulocytePrefab);
        prefabs[EntityType.Toxin] = Resources.Load<GameObject>(PathToToxinPrefab);
        prefabs[EntityType.Mineral] = Resources.Load<GameObject>(PathToMineralPrefab);

        return prefabs;
    }


    // TODO: Refactor, init in 2 places
    private void SetFrequencies()
    {
        spawnFrequencies = new Dictionary<EntityType, float>();

        spawnFrequencies[EntityType.Infection] = 3;
        spawnFrequencies[EntityType.Toxin] = 14;
        spawnFrequencies[EntityType.Mineral] = 0.5f;

        elapsedTimeSinceLastSpawn = new Dictionary<EntityType, float>();
        foreach (EntityType type in spawnFrequencies.Keys)
        {
            elapsedTimeSinceLastSpawn[type] = 0;
        }
    }

    private void InitFromDB()
    {
        // TODO: Think about saving num of lvl
        LevelData level = Loader.GetLevel(0);
        heart.GetComponent<Base>().Force = level.heart.force;
        heart.GetComponent<Base>().Money = level.heart.money;

        foreach (EntityType type in prefabs.Keys)
        {
            if (type.Equals(EntityType.Mineral))
            {
                continue;
            }

            if (IsAlly(type))
            {
                SetAllyInfo(type, level);
            }
            else
            {
                SetEnemyInfo(type, level);
            }
        }
    }

    private void SetEnemyInfo(EntityType type, LevelData level)
    {
        Enemy enemy = GetEnemyInfo(type, level);
        spawnFrequencies[type] = enemy.frequency;
        prefabs[type].GetComponent<Unit>().Init(enemy.speed, enemy.force);
    }

    private void SetAllyInfo(EntityType type, LevelData level)
    {
        Ally ally = GetAllyInfo(type, level);
        prefabs[type].GetComponent<Unit>().Init(ally.speed, ally.force);
        heart.GetComponent<Base>().AddPrice(type, ally.price);
        // TODO: InitialC and timeToSpawn ignored now (
    }

    private static Enemy GetEnemyInfo(EntityType entity, LevelData level)
    {
        string entityStr = entity.ToString();
        foreach (Enemy enemy in level.enemies)
        {
            if (entityStr.Equals(enemy.name))
            {
                return enemy;
            }
        }

        return null;
    }

    private static Ally GetAllyInfo(EntityType entity, LevelData level)
    {
        string entityStr = entity.ToString();
        foreach (Ally ally in level.allies)
        {
            if (entityStr.Equals(ally.name))
            {
                return ally;
            }
        }

        return null;
    }

    private static bool IsAlly(EntityType type)
    {
        return type.Equals(EntityType.Erythrocyte) || type.Equals(EntityType.Lymfocyte) || type.Equals(EntityType.Granulocyte);
    }

    public enum EntityType
    {
        Infection,
        Toxin,
        Erythrocyte,
        Lymfocyte,
        Granulocyte,
        Mineral
    }
}
