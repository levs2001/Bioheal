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
    public static Config config;

    public static void LoadConfig()
    {
        if (config == null)
        {
            string json = File.ReadAllText(configPath);
            config = JsonConvert.DeserializeObject<Config>(json);
        }
    }

}
