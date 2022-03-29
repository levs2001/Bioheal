using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

// now just call LoadConfig in Menu Scene 
// and share config wherever needed
public class Loader
{
    private static const string configPath = "Assets/Resources/config.json";
    private ConfigJson config = null;
    private LevelData[] levels;
    private int firstNotClearedLevel = 0;

    private static Loader loader = null;
    
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
        string json = File.ReadAllText(configPath);
        config = JsonConvert.DeserializeObject<ConfigJson>(json);
        long size = config.levels.Length;
        levels = new LevelData[size];
        for (int i = 0; i < size; i++)
        {
            levels[i] = new LevelData(config.levels[i]);
        }

        for (int i = 0; i < size; i++)
        {
            if (config.levels[i].cleared == false)
            {
                firstNotClearedLevel = i;
                break;
            }
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
            throw new ArgumentException("all levels are cleared");
        }
        return levels[firstNotClearedLevel];
    }

    public void SetLevelCleared(int index)
    {
        config.levels[index].cleared = true;
        firstNotClearedLevel = index + 1;
    }

    // call when exiting game so progress's saved
    public void UpdateJson()
    {
        string jsonText = JsonConvert.SerializeObject(config);
        File.WriteAllText(configPath, jsonText);
    }
}
