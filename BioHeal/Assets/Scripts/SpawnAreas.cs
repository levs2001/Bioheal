using UnityEngine;
using System.Collections.Generic;

public class SpawnAreas
{
    private const string PathToEnemySpawnAreas = "SpawnAreas/EnemyAreas";
    private const string PathToAlliedSpawnAreas = "SpawnAreas/AlliedAreas";
    private const string PathToMineralSpawnAreas = "SpawnAreas/MineralAreas";

    private class EntitySpawnAreas
    {
        private BoxCollider2D[] entitySpawnAreas;

        public BoxCollider2D randomEntitySpawnArea
        {
            get
            {
                int spawnAreaNum = Random.Range(0, entitySpawnAreas.Length);
                return entitySpawnAreas[spawnAreaNum];
            }
        }
        public EntitySpawnAreas(string path)
        {
            entitySpawnAreas = Resources.LoadAll<BoxCollider2D>(path);
        }


    }

    public enum EntityClass
    {
        enemy,
        allied,
        mineral
    }

    private Dictionary<EntityClass, EntitySpawnAreas> areas;

    public BoxCollider2D GetRandomArea(EntityClass entityClass)
    {
        return areas[entityClass].randomEntitySpawnArea;
    }

    public SpawnAreas()
    {
        areas = new Dictionary<EntityClass, EntitySpawnAreas>();

        areas[EntityClass.allied] = new EntitySpawnAreas(PathToAlliedSpawnAreas);
        areas[EntityClass.enemy] = new EntitySpawnAreas(PathToEnemySpawnAreas);
        areas[EntityClass.mineral] = new EntitySpawnAreas(PathToMineralSpawnAreas);
    }
}
