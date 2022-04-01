using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class SceneManager : MonoBehaviour
{
    private const string PathPrefabs = "Entities/";
    public static SceneManager sceneManager { get; private set; }

    private Dictionary<EntityType, EntityManager> entityManagers;

    private Dictionary<EntityType, ActionTimer> actionTimers;
    private Dictionary<EntityType, GameObject> prefabs;

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

    public Dictionary<EntityType, EntityManager> EntityManagers
    {
        get { return entityManagers; }
    }

    private void Awake()
    {
        sceneManager = this;

        LevelData level = Loader.LoaderInstance.GetLevel(0);

        InitPrefabs(level);

        heart = GameObject.FindWithTag("Heart");
        level.InitHeart(heart.GetComponent<Base>());

        entityManagers = prefabs.ToDictionary(pair => pair.Key, pair => new EntityManager(pair.Value, pair.Key));
        actionTimers = level.Frequencies.ToDictionary(pair => pair.Key, pair => new ActionTimer(pair.Value, entityManagers[pair.Key].Spawn));
    }

    private void Update()
    {
        foreach (EntityType type in actionTimers.Keys)
        {
            actionTimers[type].AddTimeAndDoAction(Time.deltaTime);
        }
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
    }
}
