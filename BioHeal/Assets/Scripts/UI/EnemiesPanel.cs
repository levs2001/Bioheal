using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SceneManager;
using static LogFactory;


public class EnemiesPanel : MonoBehaviour
{
    private static readonly Log log = LogFactory.GetLog(typeof(Base));

    [SerializeField] private Text textAmountInfections, textAmountToxins;
    private int amountInfections, amountToxins;
    private int initialAmountInfections, initialAmountToxins;

    private static EnemiesPanel instance = null;
    public static EnemiesPanel Instance
    {
         get
        {
            if (instance == null)
            {
                log.Error(new System.Exception("EnemiesPanel does not exist")); 
            }
            
            return instance;
        }
    }
    public void UpdateAmountOfEnemies(EntityType unitType)
    {
        if (unitType == EntityType.Toxin)
        {
            if (amountToxins > 0)
                --amountToxins;
            textAmountToxins.text = $"{amountToxins}" + $"/" + $"{initialAmountToxins}";
        }
        else if (unitType == EntityType.Infection)
        {
            if (amountInfections > 0)
                --amountInfections;
            textAmountInfections.text = $"{amountInfections}" + $"/" + $"{initialAmountInfections}";
        }
    }
    
    void Start()
    {
        instance = this;
        
        initialAmountToxins = SceneManager.sceneManager.GetAmountOfEnemies(EntityType.Toxin);
        initialAmountInfections = SceneManager.sceneManager.GetAmountOfEnemies(EntityType.Infection);
        amountToxins = initialAmountToxins;
        amountInfections = initialAmountInfections;
        textAmountToxins.text = $"{amountToxins}" + $"/" + $"{initialAmountToxins}";
        textAmountInfections.text = $"{amountInfections}" + $"/" + $"{initialAmountInfections}";
    }

}
