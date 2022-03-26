public class Infection : Unit
{
    private new void Start()
    {
        base.Start();
        entityType = EntityType.Infection;
        aim = SceneManager.sceneManager.Heart;
    }
    private void FixedUpdate()
    {
        Move();
    }
}
