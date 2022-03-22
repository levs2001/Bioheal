using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData 
{
    private LevelJson level;
    private Dictionary<EntityType, AllyJson> allyInfo;
    
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
            prices.Add(Enum.TryParse(ally.name, out EntityType entity), ally.price);
        }
        return prices;
    }

    public Dictionary<EntityType, int> GetTimeToSpawn() 
    {
        Dictionary<EntityType, int> timeToSpawn = new Dictionary<EntityType, int>();
        foreach (AllyJson ally in level.allies)
        {
            prices.Add(Enum.TryParse(ally.name, out EntityType entity), ally.timeToSpawn);
        }
        return timeToSpawn;
    }

     public Dictionary<EntityType, int> GetFrequencies() 
    {
        Dictionary<EntityType, int> frequency = new Dictionary<EntityType, int>();
        foreach (EnemyJson enemy in level.enemy)
        {
            prices.Add(Enum.TryParse(enemy.name, out EntityType entity), enemy.frequency);
        }
        return frequency;
    }

    private void SetAllyInfo()
    {
        foreach (AllyJson ally in level.allies)
        {
            allyInfo.Add(Enum.TryParse(ally.name, out EntityType entity), ally);
        }
    }

    public float GetAllySpeed(EntityType ally)
    {
        return allyInfo[ally].speed; 
    }
    
    public float GetAllyForce(EntityType ally)
    {
        return allyInfo[ally].force; 
    }
}
