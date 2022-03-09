using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.IO;

public class MineralTest
{
    private MineralManager mineralManager;

    [SetUp]
    public void Setup()
    {
        GameObject gameObject = GameObject.Instantiate(new GameObject());
        mineralManager = gameObject.AddComponent<MineralManager>();

        mineralManager.Prefab = Resources.Load("Prefabs/Mineral") as GameObject;
        mineralManager.SpawnArea = gameObject.AddComponent<BoxCollider2D>();

        mineralManager.SpawnArea.size = new Vector2(10, 10);
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(mineralManager);
    }

    [UnityTest]
    public IEnumerator MineralBeforeSpawnCountTest()
    {

        yield return new WaitForSeconds(mineralManager.RespawnTime - 0.01f);
        Assert.AreEqual(mineralManager.FreeMinerals.Count, 0);
    }

    [UnityTest]
    public IEnumerator MineralSpawnCountTest()
    {
        yield return new WaitForSeconds(mineralManager.RespawnTime);
        Assert.AreEqual(mineralManager.FreeMinerals.Count, mineralManager.MaxCount);
    }

    [UnityTest]
    public IEnumerator GetAimTest()
    {
        yield return new WaitForSeconds(mineralManager.RespawnTime);
        GameObject closestMineral = mineralManager.getAim(new Vector3(0, 0, 0));

        Assert.NotNull(closestMineral);
        Assert.NotNull(MineralManager.instance);

        float dist = Vector3.Distance(new Vector3(0, 0, 0), closestMineral.transform.position);
        Debug.Log(dist);
        foreach (GameObject mineral in mineralManager.FreeMinerals)
        {
            Debug.Log(Vector3.Distance(mineral.transform.position, new Vector3(0, 0, 0)));

            Assert.LessOrEqual(dist, Vector3.Distance(mineral.transform.position, new Vector3(0, 0, 0)));
        }
    }

    [UnityTest]
    public IEnumerator DestroyMineralTest()
    {
        yield return new WaitForSeconds(mineralManager.RespawnTime);

    }
}