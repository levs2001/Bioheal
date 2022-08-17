using UnityEngine;

public class Lymphocyte : Warrior
{
    new private void Start()
    {
        base.Start();
        entityType = EntityType.Lymphocyte;
        aim = new Aim(EntityType.Toxin);
    }
}
