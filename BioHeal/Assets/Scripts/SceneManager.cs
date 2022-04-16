using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class SceneManager : MonoBehaviour
{
    private const string PathPrefabs = "Entities/";
    private const string PathHealthDisplayPrefabs = "Entities/HealthDisplay/";
    public static SceneManager sceneManager { get; private set; }

    private Dictionary<EntityType, EntityManager> entityManagers;

    private Dictionary<EntityType, GameObject> prefabs;
    public GameObject healthbarPrefab;

    private Dictionary<EntityType, float> timeToSpawn;

    private Dictionary<EntityType, int> amountEnemiesPerLevel;

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

    public Dictionary<EntityType, float> TimeToSpawn
    {
        get { return timeToSpawn; }
    }

    private void Awake()
    {
        sceneManager = this;

        LevelData level = Loader.LoaderInstance.GetLevel(0);

        InitPrefabs(level);

        heart = GameObject.FindWithTag("Heart");
        level.InitHeart(heart.GetComponent<Base>());

        entityManagers = prefabs.ToDictionary(pair => pair.Key, pair => new EntityManager(pair.Value, pair.Key));

        amountEnemiesPerLevel = level.AmountEnemiesPerLevel;

        foreach (EntityType type in amountEnemiesPerLevel.Keys)
        {
            ActionTimer actionTimer = new GameObject(type.ToString() + "Timer").AddComponent<ActionTimer>();
            actionTimer.Timer = level.Frequencies[type];
            actionTimer.SomeAction = entityManagers[type].Spawn;
            actionTimer.SomeAction = (() => amountEnemiesPerLevel[type]--);
            actionTimer.SomeAction = (() =>
            {
                if (amountEnemiesPerLevel[type] == 0)
                {
                    Destroy(actionTimer.gameObject);
                }
            });
        }

        ActionTimer actionTimerForMineral = new GameObject(EntityType.Mineral.ToString() + "Timer").AddComponent<ActionTimer>();
        actionTimerForMineral.Timer = level.Frequencies[EntityType.Mineral];
        actionTimerForMineral.SomeAction = entityManagers[EntityType.Mineral].Spawn;

        timeToSpawn = level.TimeToSpawn;

        foreach (EntityType type in level.InitialCount.Keys)
        {
            for (int i = 0; i < level.InitialCount[type]; i++)
            {

                entityManagers[type].Spawn();
            }
        }

        SoundManager.Instance.PlaySound(SoundManager.SoundType.MainTheme);
    }

    public void SpawnEntity(EntityType entityType, Vector3? position = null)
    {
        if (position.HasValue)
            entityManagers[entityType].SpawnByCoordinates(position.Value);
        else
            entityManagers[entityType].Spawn();
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

    public void TransferEntityFromBusyToFree(EntityType managerType, GameObject entity)
    {
        entityManagers[managerType].TransferFromBusyToFree(entity);
    }

    private void InitPrefabs(LevelData level)
    {
        prefabs = new Dictionary<EntityType, GameObject>();

        foreach (EntityType type in Enum.GetValues(typeof(EntityType)))
        {
            if (type == EntityType.Heart)
                continue;

            prefabs[type] = Resources.Load<GameObject>(PathPrefabs + type.ToString());
        }

        level.InitUnits(prefabs);
        healthbarPrefab = Resources.Load<GameObject>(PathHealthDisplayPrefabs + Loader.LoaderInstance.healthDisplayType.ToString());
    }
}
