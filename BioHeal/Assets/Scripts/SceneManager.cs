using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class SceneManager : MonoBehaviour
{
    private const string PathToErythrocytePrefab = "Entities/Erythrocyte";
    private const string PathToGranulocytePrefab = "Entities/Granulocyte";
    private const string PathToHeartPrefab = "Entities/Heart";
    private const string PathToInfectionPrefab = "Entities/Infection";
    private const string PathToLymfocytePrefab = "Entities/Lymfocyte";
    private const string PathToMineralPrefab = "Entities/Mineral";
    private const string PathToToxinPrefab = "Entities/Toxin";
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
        get { return heart; }
    }

    //Используется для того, чтобы инфекция знала позицию базы
    public Vector3? HeartPosition
    {
        get { return heart != null ? (Vector3?)heart.transform.position : null; }
    }

    private void Awake()
    {
        sceneManager = this;
        spawnAreas = new SpawnAreas();

        LevelData level = Loader.LoaderInstance.GetLevel(0);

        // TODO: подумать об оъединении 2х этих сущностей в один класс
        spawnFrequencies = level.Frequencies;
        elapsedTimeSinceLastSpawn = spawnFrequencies.Keys.ToDictionary(type => type, type => 0f);

        InitPrefabs(level);

        heart = GameObject.FindWithTag("Heart");
        level.InitHeart(heart.GetComponent<Base>());

        entityManagers = prefabs.ToDictionary(pair => pair.Key, pair => new EntityManager(pair.Value));
    }

    private void Update()
    {
        //A copy of the keys is made, since it is impossible to change the value by key and iterate over the dictionary at the same time
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
        // TODO: В EntityManager перенести SpawnArea. SpawnArea лучше перенести в Prefab. От этого метода избавиться
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

    private void InitPrefabs(LevelData level)
    {
        prefabs = new Dictionary<EntityType, GameObject>();

        prefabs[EntityType.Infection] = Resources.Load<GameObject>(PathToInfectionPrefab);
        prefabs[EntityType.Erythrocyte] = Resources.Load<GameObject>(PathToErythrocytePrefab);
        prefabs[EntityType.Lymfocyte] = Resources.Load<GameObject>(PathToLymfocytePrefab);
        prefabs[EntityType.Granulocyte] = Resources.Load<GameObject>(PathToGranulocytePrefab);
        prefabs[EntityType.Toxin] = Resources.Load<GameObject>(PathToToxinPrefab);
        prefabs[EntityType.Mineral] = Resources.Load<GameObject>(PathToMineralPrefab);

        level.InitUnits(prefabs);
    }
}
