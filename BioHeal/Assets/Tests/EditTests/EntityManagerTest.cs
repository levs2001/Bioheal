using NUnit.Framework;
using UnityEngine;

public class EntityManagerTest
{
    private const string PathToErythrocytePrefab = "Entities/Erythrocyte";
    private EntityManager entityManager;

    [SetUp]
    public void SetUp()
    {
        GameObject prefab = Resources.Load<GameObject>(PathToErythrocytePrefab);
        entityManager = new EntityManager(prefab);
    }

    [Test]
    public void EntitySpawnTest()
    {
        GameObject gameObject = new GameObject();
        BoxCollider2D spawnArea = gameObject.AddComponent<BoxCollider2D>();
        spawnArea.size = new Vector2(10, 10);
        entityManager.Spawn(spawnArea);

        Assert.NotNull(entityManager.FreeEntities);
        Assert.AreEqual(1, entityManager.FreeEntities.Count);
    }

    [Test]
    public void EntityGetClosestEntityTest()
    {
        GameObject gameObject = new GameObject();
        BoxCollider2D spawnArea = gameObject.AddComponent<BoxCollider2D>();
        spawnArea.size = new Vector2(10, 10);
        entityManager.Spawn(spawnArea);
        entityManager.Spawn(spawnArea);

        Vector3 hunter = new Vector3(0, 0, 0);
        GameObject closestEntity = entityManager.GetClosestEntity(hunter);

        Assert.NotNull(closestEntity);

        Assert.AreEqual(1, entityManager.FreeEntities.Count);

        Assert.AreEqual(1, entityManager.BusyEntities.Count);

        Assert.LessOrEqual(Vector3.Distance(entityManager.BusyEntities[0].transform.position, hunter),
                                 Vector3.Distance(entityManager.FreeEntities[0].transform.position, hunter));
    }

    [Test]
    public void DeleteEntityTest()
    {
        GameObject gameObject = new GameObject();
        BoxCollider2D spawnArea = gameObject.AddComponent<BoxCollider2D>();
        spawnArea.size = new Vector2(10, 10);
        entityManager.Spawn(spawnArea);
        entityManager.Spawn(spawnArea);

        Vector3 hunter = new Vector3(0, 0, 0);
        GameObject closestEntity = entityManager.GetClosestEntity(hunter);

        entityManager.DeleteObject(closestEntity);
        Assert.AreEqual(1, entityManager.FreeEntities.Count);

        Assert.AreEqual(0, entityManager.BusyEntities.Count);
    }
}
