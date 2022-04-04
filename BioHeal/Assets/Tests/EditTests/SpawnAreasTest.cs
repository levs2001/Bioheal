using NUnit.Framework;

public class SpawnAreasTest
{
    [Test]
    public void GetRandomEnemyAreaTest()
    {
        SpawnAreas spawnAreas = new SpawnAreas(EntityType.Erythrocyte);
        Assert.IsNotNull(spawnAreas.randomEntitySpawnArea);
    }
}
