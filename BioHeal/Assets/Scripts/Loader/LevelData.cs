using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelData
{
    private LevelJson level;
    private Dictionary<EntityType, UnitJson> unitsInfo = new Dictionary<EntityType, UnitJson>();
    private Dictionary<EntityType, float> frequency = new Dictionary<EntityType, float>();
    private Dictionary<EntityType, float> timeToSpawn = new Dictionary<EntityType, float>();
    private Dictionary<EntityType, int> prices = new Dictionary<EntityType, int>();
    private Dictionary<EntityType, int> initialCount = new Dictionary<EntityType, int>();
    private Dictionary<EntityType, int> amountEnemiesPerLevel = new Dictionary<EntityType, int>();

    public Dictionary<EntityType, float> Frequencies
    {
        get { return frequency; }
    }

    public Dictionary<EntityType, float> TimeToSpawn
    {
        get { return timeToSpawn; }
    }

    public Dictionary<EntityType, int> InitialCount
    {
        get { return initialCount; }
    }

    public Dictionary<EntityType, int> AmountEnemiesPerLevel
    {
        get { return amountEnemiesPerLevel; }
    }

    public LevelData(LevelJson level)
    {
        this.level = level;
        SetUnitsInfo();
    }

    public void InitHeart(Base heart)
    {
        heart.Force = level.heart.force;
    }

    public void InitShopPanel(ShopPanel panel)
    {
        panel.Money = level.heart.money;
        panel.Prices = prices;
    }

    public void InitUnits(Dictionary<EntityType, GameObject> prefabs)
    {
        // TODO: Think about saving num of lvl            
        HashSet<EntityType> units = new HashSet<EntityType>(prefabs.Keys);
        units.Remove(EntityType.Mineral);
        foreach (EntityType unit in units)
        {
            InitSingleUnit(unit, prefabs[unit].GetComponent<Unit>());
        }
    }

    private void SetUnitsInfo()
    {
        foreach (AllyJson ally in level.allies)
        {
            EntityType entityType = (EntityType)Enum.Parse(typeof(EntityType), ally.name);
            unitsInfo.Add(entityType, ally);
            prices.Add(entityType , ally.price);
            timeToSpawn.Add(entityType, ally.timeToSpawn);
            initialCount.Add(entityType, ally.initialC);
        }

        foreach (EnemyJson enemy in level.enemies)
        {
            EntityType entityType = (EntityType)Enum.Parse(typeof(EntityType), enemy.name);
            unitsInfo.Add(entityType, enemy);
            frequency.Add(entityType, enemy.frequency);
            amountEnemiesPerLevel.Add(entityType, enemy.amountPerLevel);
        }

        frequency.Add(EntityType.Mineral, level.mineral.frequency);
        initialCount.Add(EntityType.Mineral, level.mineral.initialC);
    }

    private void InitSingleUnit(EntityType type, Unit unit)
    {
        unit.Init(unitsInfo[type].speed, unitsInfo[type].force);
    }
}
