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


    public void InitHeart(Base heart)
    {
        heart.Force = GetHeartForce();
        heart.Money = GetHeartMoney();
        heart.SetPrices(prices);
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

    public Dictionary<EntityType, float> GetFrequencies()
    {
        return frequency;
    }

    public Dictionary<EntityType, int> GetTimeToSpawn()
    {
        return timeToSpawn;
    }

    public Dictionary<EntityType, float> GetElapsedTimeSinceSpawn()
    {
        return elapsedTimeSinceLastSpawn;
    }

    public LevelData(LevelJson level)
    {
        this.level = level;
        SetUnitsInfo();
        SetTimeToSpawn();
        SetFrequencies();
        SetPrices();
        SetElapsedTimeSinceSpawn();
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

    public void SetElapsedTimeSinceSpawn()
    {
        foreach (EnemyJson enemy in level.enemies)
        {
            EntityType entityType = (EntityType)Enum.Parse(typeof(EntityType), enemy.name);
            elapsedTimeSinceLastSpawn.Add(entityType, 0);
        }
        elapsedTimeSinceLastSpawn.Add(EntityType.Mineral, 0);
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
        int force = GetUnitForce(type);
        float speed = GetUnitSpeed(type);
        unit.Init(speed, force);
        // TODO: InitialC and timeToSpawn ignored now (
    }

    private float GetUnitSpeed(EntityType unit)
    {
        return unitsInfo[unit].speed;
    }

    private int GetUnitForce(EntityType unit)
    {
        return unitsInfo[unit].force;
    }

    private int GetHeartForce()
    {
        return level.heart.force;
    }

    private int GetHeartMoney()
    {
        return level.heart.money;
    }
}
