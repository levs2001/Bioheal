using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

public class Loader : MonoBehaviour
{
    
    private const string configPath = "Assets/Resources/config.json";
    public static Config config;

    void Awake()
    {
        string json = File.ReadAllText(configPath);
        config = JsonConvert.DeserializeObject<Config>(json);
        Debug.Log(config.levels[0].minerals.initialC);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
