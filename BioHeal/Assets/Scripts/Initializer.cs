using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class Initializer 
{
    public static void InitHeart(Base heart, LevelData level)
    {
        heart.Force = level.GetHeartForce();
        heart.Money = level.GetHeartMoney();
        var prices = level.GetPrices();
        heart.SetPrices(prices);
    }

    public static Dictionary<EntityType, float> InitFrequencies(LevelData level)
    {
        return level.GetFrequencies();
    }

    public static Dictionary<EntityType, float> InitElapsedTimeSinceSpawn(LevelData level, HashSet<EntityType> types)
    {
        var elapsedTimeSinceLastSpawn = new Dictionary<EntityType, float>();
        foreach (EntityType type in types)
        {
            elapsedTimeSinceLastSpawn[type] = 0;
        }
        return elapsedTimeSinceLastSpawn;
    }

    public static void InitUnits(LevelData level, Dictionary<EntityType, GameObject> prefabs)
    {
    // TODO: Think about saving num of lvl            
        HashSet<EntityType> units = new HashSet<EntityType>(prefabs.Keys);
        units.Remove(EntityType.Mineral);
        foreach (EntityType unit in units)
        {
            SetUnitInfo(unit, prefabs[unit].GetComponent<Unit>(), level);
        }

    }

    private static void SetUnitInfo(EntityType type, Unit unit, LevelData level)
    {
        int force = level.GetUnitForce(type);
        float speed = level.GetUnitSpeed(type);
        unit.Init(speed, force);
        // TODO: InitialC and timeToSpawn ignored now (
    }
}


