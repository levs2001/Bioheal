using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SceneManager;
using static MetaInfo;

public class Base : Alive
{
    //SerializeField, because I am initializing these fields from Unity API from inspector 
    [SerializeField] private GameObject menuBase;
    // [SerializeField] private Text textMoneyMenu;
    // [SerializeField] private Text textLimfo, textGranulo, textEritro;
    // [SerializeField] private GameObject unitInfo;
    // [SerializeField] private Text textInfo;
    [SerializeField] private Text textForceBase;

    [SerializeField] private Text textAmountInfections, textAmountToxins;
    private int amountInfections, amountToxins;
    private int initialAmountInfections, initialAmountToxins;

    //to open and close menuBase at the end of the level
    private static Base instance = null;
    public static Base Instance
    {
        get
        {
            if (instance == null)
                throw new System.Exception("Base does not exist");
            else
                return instance;
        }
    }

    public void UpdateAmountOfEnemies(EntityType unitType)
    {
        if (unitType == EntityType.Toxin)
        {
            --amountToxins;
            textAmountToxins.text = $"{amountToxins}" + $"/" + $"{initialAmountToxins}";
        }
        else if (unitType == EntityType.Infection)
        {
            --amountInfections;
            textAmountInfections.text = $"{amountInfections}" + $"/" + $"{initialAmountInfections}";
        }
    }

    private void ChangeForceTextAndCloseMenuIfNeeded()
    {
        textForceBase.text = force < 0 ? $"{0}" : $"{force}";
    }

    private void Die()
    {
        //this action moved to EndLevel to remember timeScale to return it
        //after closing EndLevelMenu
        //Time.timeScale = 0;

        EndLevel.Instance.OpenLoseLevelMenu();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        //Init();
        instance = this;

        base.Start();
        textForceBase.text = $"{force}";

        initialAmountToxins = SceneManager.sceneManager.GetAmountOfEnemies(EntityType.Toxin);
        initialAmountInfections = SceneManager.sceneManager.GetAmountOfEnemies(EntityType.Infection);
        amountToxins = initialAmountToxins;
        amountInfections = initialAmountInfections;
        textAmountToxins.text = $"{amountToxins}" + $"/" + $"{initialAmountToxins}";
        textAmountInfections.text = $"{amountInfections}" + $"/" + $"{initialAmountInfections}";

        menuBase.SetActive(false);

        entityTakeDamageEvent += ChangeForceTextAndCloseMenuIfNeeded;
        entityTakeDamageEvent += (() => SoundManager.Instance.PlaySound(SoundManager.SoundType.HeartDamage));
        entityTakeDamageEvent += (() =>
        {
            if (force <= 0)
            {
                SoundManager.Instance.PlaySound(SoundManager.SoundType.HeartDead);
                Die();
            }
        });
    }
}