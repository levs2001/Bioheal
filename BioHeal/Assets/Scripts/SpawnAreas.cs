using UnityEngine;

class SpawnAreas
{
    private const string PathToEnemySpawnAreas = "SpawnAreas/EnemyAreas";
    private const string PathToAlliedSpawnAreas = "SpawnAreas/AlliedAreas";
    private const string PathToMineralSpawnAreas = "SpawnAreas/MineralAreas";
    [SerializeField] private BoxCollider2D[] entitySpawnAreas;

    public BoxCollider2D randomEntitySpawnArea
    {
        get
        {
            int spawnAreaNum = Random.Range(0, entitySpawnAreas.Length);
            return entitySpawnAreas[spawnAreaNum];
        }
    }
    public SpawnAreas(EntityType entityType)
    {
        switch (entityType)
        {
            case EntityType.Erythrocyte:
            case EntityType.Lymphocyte:
            case EntityType.Granulocyte:
                entitySpawnAreas = Resources.LoadAll<BoxCollider2D>(PathToAlliedSpawnAreas);
                break;
            case EntityType.Infection:
            case EntityType.Toxin:
                entitySpawnAreas = Resources.LoadAll<BoxCollider2D>(PathToEnemySpawnAreas);
                break;
            case EntityType.Mineral:
                entitySpawnAreas = Resources.LoadAll<BoxCollider2D>(PathToMineralSpawnAreas);
                break;
            default:
                break;
        }
    }
}