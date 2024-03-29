using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using static EndLevel;
using static ShopPanel;
using static PauseMenu;

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

    private bool endLevelMenuIsOpened = false;

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

        //load current level from loader
        LevelData level = Loader.LoaderInstance.GetLevel(Loader.LoaderInstance.CurrentLevel);

        InitPrefabs(level);

        heart = GameObject.FindWithTag("Heart");
        level.InitHeart(heart.GetComponent<Base>());
        level.InitShopPanel(GameObject.FindWithTag("ShopPanel").GetComponent<ShopPanel>());

        entityManagers = prefabs.ToDictionary(pair => pair.Key, pair => new EntityManager(pair.Value, pair.Key));
        entityManagers[EntityType.Mineral] = new MineralManager(prefabs[EntityType.Mineral], EntityType.Mineral);

        amountEnemiesPerLevel = new Dictionary<EntityType, int>(level.AmountEnemiesPerLevel);

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

        endLevelMenuIsOpened = false;
    }

    private void Update()
    {        
        if (!heart)
        {
            endLevelMenuIsOpened = true;
        }
        
        if (CheckThatAllEnemiesDestroyed())
        {
            //to open EndLevelMenu once
            if (endLevelMenuIsOpened == false)
            {
                endLevelMenuIsOpened = true;


                //this action moved to EndLevel to remember timeScale to return it
                //after closing EndLevelMenu
                //Time.timeScale = 0;
        
                PauseMenu.Instance.OpenWinLevelMenu();

                // EndLevel.Instance.OpenWinLevelMenu();
            }
        }

        //open pause menu using Escape button
        if (Input.GetKeyDown(KeyCode.Escape) &&
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == $"GameScene")
        {
            PauseMenu.Instance.EscapeButton();
        }
    }

    public void SpawnEntity(EntityType entityType, Vector3? position = null)
    {
        if (position.HasValue)
            entityManagers[entityType].SpawnByCoordinates(position.Value);
        else
            entityManagers[entityType].Spawn();
    }

    public GameObject GetAim(EntityType aimType, Vector3 finderPosition, int damage)
    {
        EntityManager entityManager = entityManagers[aimType];
        GameObject aim = entityManager.GetClosestEntity(finderPosition);

        if (aim != null)
        {
            if (aimType == EntityType.Mineral)
                entityManager.TransferFromFreeToBusy(aim);
            else
            {
                Alive aimAlive = (Alive)aim.GetComponent(typeof(Alive));
                aimAlive.AddPotentialDamage(damage);
                if (aimAlive.PotentialDamage >= aimAlive.Force)
                {
                    entityManager.TransferFromFreeToBusy(aim);
                }
            }
        }


        return aim;
    }

    public void DeleteObject(EntityType entityType, GameObject objectToDelete)
    {
        EntityManager entityManager = entityManagers[entityType];
        entityManager.DeleteObject(objectToDelete);
        Destroy(objectToDelete);

        //Update amount of alive enemies
        if (entityType == EntityType.Toxin || entityType == EntityType.Infection)
        {
            EnemiesPanel.Instance.UpdateAmountOfEnemies(entityType);
        }
    }

    public int GetAmountOfEnemies(EntityType type)
    {
        return amountEnemiesPerLevel[type];
    }

    public void TransferEntityFromBusyToFree(EntityType managerType, GameObject entity)
    {
        entityManagers[managerType].TransferFromBusyToFree(entity);
    }

    private bool CheckThatAllEnemiesDestroyed()
    {
        if (amountEnemiesPerLevel[EntityType.Infection] != 0 || entityManagers[EntityType.Infection].FreeEntities.Count != 0
            || entityManagers[EntityType.Infection].BusyEntities.Count != 0)
        {
            return false;
        }

        return true;
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
