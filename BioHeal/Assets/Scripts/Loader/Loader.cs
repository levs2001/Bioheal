using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;

// now just call LoadConfig in Menu Scene 
// and share config wherever needed
public class Loader
{
    public const int MAIN_MENU_SCENE = 0;
    public const int GAME_SCENE = 1;
    private const int ALL_LEVELS_ARE_CLEARED = -1;
    private const string configResPath = "config";
    private readonly string configPath = Application.persistentDataPath + "/config.json";
    private static readonly Log log = LogFactory.GetLog(typeof(SoundManager));
    private ConfigJson config = null;
    private LevelData defaultLevel;
    private LevelData[] levels;
    private int firstNotClearedLevel = 0;
    private int currentLevel;
    private long amountOfLevels;
    private static Loader loader = null;
    public HealthDisplayType healthDisplayType = HealthDisplayType.None;

    public static Loader LoaderInstance
    {
        get
        {
            if (loader == null)
            {
                loader = new Loader();
            }
            return loader;
        }
    }

    private Loader()
    {
        // Loading config from resource
        string json;
        if (File.Exists(configPath))
        {
            json = File.ReadAllText(configPath);
        }
        else
        {
            json = (Resources.Load<TextAsset>(configResPath)).ToString();
        }

        config = JsonConvert.DeserializeObject<ConfigJson>(json,
                new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Populate });
        long size = config.levels.Length;
        amountOfLevels = size - 1; //start numeration with 0
        levels = new LevelData[size];
        defaultLevel = new LevelData(config.defaultLevel);
        for (int i = 0; i < size; i++)
        {
            // even if you wanna inherit unit's values from default level
            // you need to add empty unit with name value 
            // so thats loader knows this unit must present at level
            levels[i] = createLevel(config.levels[i], config.defaultLevel);
        }

        firstNotClearedLevel = ALL_LEVELS_ARE_CLEARED;
        for (int i = 0; i < size; i++)
        {
            if (config.levels[i].cleared == false)
            {
                firstNotClearedLevel = i;
                break;
            }
        }
    }

    public static LevelData createLevel(LevelJson level, LevelJson defaultLevel)
    {
        FieldInfo[] fields = typeof(LevelJson).GetFields();
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(bool)) // skip 'cleared' field
            {
                continue;
            }

            object value = field.GetValue(level);
            object defValue = field.GetValue(defaultLevel);

            if (value == null)
            {
                field.SetValue(level, defValue);
            }
            else
            {
                if (field.FieldType == typeof(AllyJson[]) || field.FieldType == typeof(EnemyJson[]))
                {
                    var units = new List<UnitJson>((IEnumerable<UnitJson>)value);
                    for (int i = 0; i < units.Count; i++)
                    {
                        UnitJson defUnit = findUnitByName((IEnumerable)defValue, units[i].name);
                        fillObject(units[i], defUnit);
                    }
                }
                else
                {
                    fillObject(value, defValue);
                }
            }
        }
        return new LevelData(level);
    }

    private static UnitJson findUnitByName(IEnumerable units, string name)
    {
        foreach (var unit in units)
        {
            UnitJson uni = (UnitJson)unit;
            if (uni.name == name)
            {
                return uni;
            }
        }
        return null;
    }

    private static void fillObject(object obj, object defObj)
    {

        FieldInfo[] fields;
        if (obj is AllyJson)
        {
            fields = typeof(AllyJson).GetFields();
        }
        else if (obj is EnemyJson)
        {
            fields = typeof(EnemyJson).GetFields();
        }
        else if (obj is HeartJson)
        {
            fields = typeof(HeartJson).GetFields();
        }
        else
        {
            fields = typeof(MineralJson).GetFields();
        }

        for (int i = 0; i < fields.Length; i++)
        {
            if (fields[i].Name == "name")
            {
                continue;
            }
            var value = fields[i].GetValue(obj);
            if ((fields[i].FieldType == typeof(float) && (float)value == -1)
                || (fields[i].FieldType == typeof(int) && (int)value == -1)) // signals that we need to use value from default level
            {
                fields[i].SetValue(obj, fields[i].GetValue(defObj));
            }
        }
    }



    //Setting this in ChooseLevel.cs, getting in SceneManager.cs
    public int CurrentLevel
    {
        set
        {
            currentLevel = value;
        }

        get
        {
            return currentLevel;
        }
    }

    //Getting this in MainMenu.cs
    public int FirstNotClearedLevel
    {
        get
        {
            return firstNotClearedLevel;
        }
    }

    //getting this in ChooseLevel.cs
    public long AmountOfLevels
    {
        get
        {
            return amountOfLevels;
        }
    }

    public LevelData GetLevel(int num)
    {
        // TODO: Merge with default not filled fields
        return levels[num];
    }

    public List<LevelData> GetClearedLevels()
    {
        List<LevelData> clearedLevels = new List<LevelData>();
        for (int i = 0; i < levels.Length; i++)
        {
            if (config.levels[i].cleared == true)
            {
                clearedLevels.Add(levels[i]);
            }
        }
        return clearedLevels;
    }

    // throws Exception if all levels are cleared
    public LevelData GetFirstNotClearedLevel()
    {
        if (firstNotClearedLevel >= levels.Length)
        {
            log.Error(new ArgumentException("all levels are cleared"));
        }
        return levels[firstNotClearedLevel];
    }

    public void SetLevelCleared(int index)
    {
        config.levels[index].cleared = true;

        //if all levels are cleared
        if (firstNotClearedLevel == ALL_LEVELS_ARE_CLEARED || firstNotClearedLevel == amountOfLevels)
            firstNotClearedLevel = ALL_LEVELS_ARE_CLEARED;
        //if we cleared last opened level
        else if (firstNotClearedLevel == index)
            firstNotClearedLevel = index + 1;
    }

    public bool AreAllLevelsCleared()
    {
        return (firstNotClearedLevel == ALL_LEVELS_ARE_CLEARED) ? true : false;
    }

    public bool IsItLastLevel()
    {
        return currentLevel == amountOfLevels;
    }

    // call when exiting game so progress's saved
    public void UpdateJson()
    {
        string jsonText = JsonConvert.SerializeObject(config);
        File.WriteAllText(configPath, jsonText);
    }

}
