using UnityEngine;

public class Infection : Warrior
{
    private new void Start()
    {
        base.Start();
        entityType = EntityType.Infection;
        aim = new Aim(EntityType.Heart, SceneManager.sceneManager.Heart);
    }

}
