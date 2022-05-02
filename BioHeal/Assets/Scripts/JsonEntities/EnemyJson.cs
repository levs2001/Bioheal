using System.ComponentModel;

public class EnemyJson : UnitJson
{
    [DefaultValue(-1f)]
    public float frequency;

    [DefaultValue(-1)]
    public int amountPerLevel;
}
