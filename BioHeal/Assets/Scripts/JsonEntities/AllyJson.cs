using System.ComponentModel;

public class AllyJson : UnitJson
{
    [DefaultValue(-1)]  
    public int price = -1;

    [DefaultValue(-1)]
    public int initialC = -1;
    
    [DefaultValue(-1f)]
    public float timeToSpawn = -1;
}
