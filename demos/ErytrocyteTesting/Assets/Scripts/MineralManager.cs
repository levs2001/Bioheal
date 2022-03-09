using UnityEngine;
using System.Collections.Generic;

public class MineralManager : MonoBehaviour
{
    public static MineralManager instance;
    [SerializeField] private GameObject prefab;
    [SerializeField] private BoxCollider2D spawnArea;
    [SerializeField] private int maxCount = 5;

    private int curCount = 0;

    [SerializeField] private float respawnTime = 1;

    private List<GameObject> busyMinerals;
    private List<GameObject> freeMinerals;

    public GameObject Prefab
    {
        set
        {
            if (value != null)
                prefab = value;
        }
    }

    public BoxCollider2D SpawnArea
    {
        set
        {
            if (value != null)
                spawnArea = value;
        }
        get
        {
            return spawnArea;
        }
    }

    public float RespawnTime
    {
        get
        {
            return respawnTime;
        }
    }

    public List<GameObject> FreeMinerals
    {
        get
        {
            return freeMinerals;
        }
    }

    public int MaxCount
    {
        get
        {
            return maxCount;
        }
    }

    private void Start()
    {
        instance = this;

        busyMinerals = new List<GameObject>();
        freeMinerals = new List<GameObject>();
    }
    private IEnumerator<WaitForSeconds> SpawnCoroutine()
    {
        yield return new WaitForSeconds(respawnTime);
        GameObject mineral = Instantiate(prefab);

        float x = Random.Range(spawnArea.transform.position.x - spawnArea.size.x / 2, spawnArea.transform.position.x + spawnArea.size.x / 2);
        float y = Random.Range(spawnArea.transform.position.y - spawnArea.size.y / 2, spawnArea.transform.position.y + spawnArea.size.y / 2);

        mineral.transform.position = new Vector3(x, y, 0);

        freeMinerals.Add(mineral);
    }

    public void Spawn()
    {

        for (int i = curCount; i < maxCount; i++)
        {
            StartCoroutine(SpawnCoroutine());
            curCount++;
        }
    }

    private void FixedUpdate()
    {
        Spawn();
    }

    public GameObject getAim(Vector3 erythrocytePosition)
    {
        float minDistance = Mathf.Infinity;
        GameObject closestMineral = null;

        foreach (GameObject mineral in freeMinerals)
        {
            float curDistance = Vector3.Distance(mineral.transform.position, erythrocytePosition);

            if (curDistance < minDistance)
            {
                minDistance = curDistance;
                closestMineral = mineral;
            }
        }

        if (closestMineral != null)
        {
            busyMinerals.Add(closestMineral);
            freeMinerals.Remove(closestMineral);
        }
        return closestMineral;
    }

    public void deleteObject(GameObject deletingMineral)
    {
        busyMinerals.Remove(deletingMineral);
        Destroy(deletingMineral);
        curCount--;
    }

    private void OnDestroy()
    {
        foreach (GameObject mineral in freeMinerals)
        {
            Object.Destroy(mineral);
        }
        foreach (GameObject mineral in busyMinerals)
        {
            Object.Destroy(mineral);
        }

        instance = null;
    }
}
