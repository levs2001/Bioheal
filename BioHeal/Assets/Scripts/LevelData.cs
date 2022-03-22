using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelData 
{
    private LevelJson level;
    private Dictionary<EntityType, UnitJson> unitsInfo;
    
    public LevelData(LevelJson level)
    {
        this.level = level;
        SetAllyInfo();
    }
    public Dictionary<EntityType, int> GetPrices() 
    {
        Dictionary<EntityType, int> prices = new Dictionary<EntityType, int>();
        foreach (AllyJson ally in level.allies)
        {
            EntityType type = (EntityType) Enum.Parse(typeof(EntityType),  ally.name);
            prices.Add(type, ally.price);
        }
        return prices;
    }

    public Dictionary<EntityType, int> GetTimeToSpawn() 
    {
        Dictionary<EntityType, int> timeToSpawn = new Dictionary<EntityType, int>();
        foreach (AllyJson ally in level.allies)
        {
            EntityType entityType = (EntityType) Enum.Parse(typeof(EntityType),  ally.name);
            timeToSpawn.Add(entityType, ally.timeToSpawn);
        }
        return timeToSpawn;
    }

     public Dictionary<EntityType, float> GetFrequencies() 
    {
        Dictionary<EntityType, float> frequency = new Dictionary<EntityType, float>();
        foreach (EnemyJson enemy in level.enemies)
        {
            EntityType entityType = (EntityType) Enum.Parse(typeof(EntityType),  enemy.name);
            frequency.Add(entityType, enemy.frequency);
        }

        frequency.Add(EntityType.Mineral, level.mineral.frequency);
        return frequency;
    }

    private void SetAllyInfo()
    {
        foreach (AllyJson ally in level.allies)
        {
            Debug.Log(ally.name);
            EntityType entityType = (EntityType) Enum.Parse(typeof(EntityType),  ally.name);
            unitsInfo.Add(entityType, ally);
        }

        foreach (EnemyJson enemy in level.enemies)
        {
            EntityType entityType = (EntityType) Enum.Parse(typeof(EntityType),  enemy.name);
            unitsInfo.Add(entityType, enemy);
        }
        
    }

    public float GetUnitSpeed(EntityType unit)
    {
        return unitsInfo[unit].speed; 
    }
    
    public int GetUnitForce(EntityType unit)
    {
        return unitsInfo[unit].force; 
    }

    public int GetHeartForce()
    {
        return level.heart.force;
    }

    public int GetHeartMoney()
    {
        return level.heart.money;
    }
}
