using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelData
{
    private LevelJson level;
    private Dictionary<EntityType, UnitJson> unitsInfo = new Dictionary<EntityType, UnitJson>();
    private Dictionary<EntityType, float> frequency = new Dictionary<EntityType, float>();
    private Dictionary<EntityType, int> timeToSpawn = new Dictionary<EntityType, int>();
    private Dictionary<EntityType, int> prices = new Dictionary<EntityType, int>();
    private Dictionary<EntityType, float> elapsedTimeSinceLastSpawn = new Dictionary<EntityType, float>();


    public Dictionary<EntityType, float> Frequencies
    {
        get { return frequency; }
    }

    public Dictionary<EntityType, int> GetTimeToSpawn
    {
        get { return timeToSpawn; }
    }

    public LevelData(LevelJson level)
    {
        this.level = level;
        SetUnitsInfo();
        SetTimeToSpawn();
        SetFrequencies();
        SetPrices();
    }

    public void InitHeart(Base heart)
    {
        heart.Force = level.heart.force;
        heart.Money = level.heart.money;
        heart.Prices = prices;
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

    private void SetPrices()
    {
        foreach (AllyJson ally in level.allies)
        {
            EntityType type = (EntityType)Enum.Parse(typeof(EntityType), ally.name);
            prices.Add(type, ally.price);
        }
    }

    private void SetTimeToSpawn()
    {
        foreach (AllyJson ally in level.allies)
        {
            EntityType entityType = (EntityType)Enum.Parse(typeof(EntityType), ally.name);
            timeToSpawn.Add(entityType, ally.timeToSpawn);
        }
    }

    private void SetFrequencies()
    {
        foreach (EnemyJson enemy in level.enemies)
        {
            EntityType entityType = (EntityType)Enum.Parse(typeof(EntityType), enemy.name);
            frequency.Add(entityType, enemy.frequency);
        }
        frequency.Add(EntityType.Mineral, level.mineral.frequency);
    }

    private void SetUnitsInfo()
    {
        foreach (AllyJson ally in level.allies)
        {
            EntityType entityType = (EntityType)Enum.Parse(typeof(EntityType), ally.name);
            unitsInfo.Add(entityType, ally);
        }

        foreach (EnemyJson enemy in level.enemies)
        {
            EntityType entityType = (EntityType)Enum.Parse(typeof(EntityType), enemy.name);
            unitsInfo.Add(entityType, enemy);
        }
    }

    private void InitSingleUnit(EntityType type, Unit unit)
    {
        unit.Init(unitsInfo[type].speed, unitsInfo[type].force);
        // TODO: InitialC and timeToSpawn ignored now (
    }
}
