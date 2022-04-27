using System.ComponentModel;

public class UnitJson
{
   public string name;
   
   [DefaultValue(-1f)]
   public float speed = -1;

   [DefaultValue(-1)]
   public int force = -1;
}
