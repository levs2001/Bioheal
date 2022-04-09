using UnityEngine;

public class Infection : Warrior
{
    private new void Start()
    {
        base.Start();
        entityType = EntityType.Infection;
        aim = new Aim(EntityType.Heart, SceneManager.sceneManager.Heart);
    }

    protected override void FindNewAimIfNeeded()
    {
        // This method is override from Unit, due to uselessness for infection
        // The aim for infection can be init once in start
        // If this method won't be override it cause exception.
    }
}
