using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralManager : EntityManager
{
    //degrees to rotate units before spawn
    private const int CIRCLE = 360, STEP = 30;

    public MineralManager(GameObject prefab, EntityType entityType) : base(prefab, entityType) { }

    public override void SpawnByCoordinates(Vector3 position)
    {
        GameObject entity = GameObject.Instantiate(prefab);
        entity.transform.position = position;
        freeEntities.Add(entity);

        //rotate entities to look realistic
        entity.transform.Rotate(0.0f, 0.0f, STEP * Random.Range(0, CIRCLE / STEP - 1));
    }
}
