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
    private static Config config;

    public static void LoadConfig()
    {
        string json = File.ReadAllText(configPath);
        config = JsonConvert.DeserializeObject<Config>(json);
    }

    private static Config GetConfig()
    {
        if (config == null)
        {
            LoadConfig();
        }
        return config;
    }

    public static LevelData GetLevel(int num)
    {
        // TODO: Merge with default not filled fields
        return GetConfig().levels[num];
    }
}
