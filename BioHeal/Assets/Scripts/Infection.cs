using UnityEngine;

public class Infection : Warrior
{
    protected override void Start()
    {
        base.Start();
        entityType = EntityType.Infection;
        aim = new Aim(EntityType.Heart, SceneManager.sceneManager.Heart);
    }

}
