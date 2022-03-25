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
    private static string configPath = "Assets/Resources/config.json";
    private static ConfigJson config = null;
    private static LevelData[] levels;

    public static void LoadConfig()
    {
        string json = File.ReadAllText(configPath);
        config = JsonConvert.DeserializeObject<ConfigJson>(json);
        long size = config.levels.Length;
        levels = new LevelData[size];
        for (int i = 0; i < size; i++)
        {
            levels[i] = new LevelData(config.levels[i]);
        }
    }

    public static LevelData GetLevel(int num)
    {
        // TODO: Merge with default not filled fields
        if (config == null)
        {
            LoadConfig();
        }
        return levels[num];
    }
}
