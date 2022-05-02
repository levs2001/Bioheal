using System.ComponentModel;
public class LevelJson
{
   [DefaultValue(null)]  
   public EnemyJson[] enemies; 

   [DefaultValue(null)]  
   public AllyJson[] allies;

   [DefaultValue(null)]  
   public MineralJson mineral;

   [DefaultValue(null)]  
   public HeartJson heart;

   [DefaultValue(false)]  
   public bool cleared;
}
