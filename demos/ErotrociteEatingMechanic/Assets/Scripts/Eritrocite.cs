using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eritrocite : MonoBehaviour
{
    public bool isBusy = false;
    public GameObject mineral;

    public float speed;

    private void findMinerals()
    {
        mineral = null;
        GameObject[] minerals = GameObject.FindGameObjectsWithTag("Mineral");

        float distance = Mathf.Infinity;
        Vector3 position = this.transform.position;

        foreach (GameObject mineral in minerals)
        {
            Vector3 diff = mineral.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            Mineral mineralObj = mineral.GetComponent("Mineral") as Mineral;
            if (curDistance < distance && mineralObj.isBusy == false)
            {
                distance = curDistance;
                this.mineral = mineral;
            }
        }

        if (mineral != null)
        {
            Mineral mineralObj = mineral.GetComponent("Mineral") as Mineral;
            isBusy = true;
            mineralObj.isBusy = true;
        }
    }

    private void Update()
    {
        if (isBusy == false)
        {
            findMinerals();
        }

            if (mineral != null && isBusy == true)
            {
                Vector3 mineralPos = mineral.transform.position;
                Vector3 delta =  mineralPos - this.transform.position;
                delta.Normalize();
                this.transform.position += delta * speed;
            }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Mineral")
        {
            Destroy(mineral);
            isBusy = false;
            MineralManager mineralManager = FindObjectOfType<MineralManager>() as MineralManager;
            mineralManager.curCount--;
        }
    }    
}
