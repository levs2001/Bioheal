using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SceneManager;
using static Loader;

public class Base : Alive
{
    //SerializeField, because I am initializing these fields from Unity API from inspector 
    [SerializeField] private GameObject menuBase;
    [SerializeField] private Text textMoneyMenu;
    [SerializeField] private Text textLimfo, textGranulo, textEritro;
    [SerializeField] private GameObject unitInfo;
    [SerializeField] private Text textInfo;
    [SerializeField] private Text textMoneyBase, textForceBase;

    //fields at EndGameMenu
    [SerializeField] private GameObject menuEndLevel;
    [SerializeField] private Text textLoadLevel;
    [SerializeField] private Button buttonLoadLevel;
    [SerializeField] private Text textResultLevel;

    private float scale; //remember timeScale if clicking on pause or game is ended
    private int money;
    private Dictionary<EntityType, int> prices = new Dictionary<EntityType, int>();

    public int Money
    {
        set { money = value; }
    }

    public Dictionary<EntityType, int> Prices
    {
        set { prices = value; }
    }

    ///////         Public methods, called from buttons         ///////
    public void OpenMenu()
    {
        if (!menuBase.activeSelf)
        {
            menuBase.SetActive(true);
            SoundManager.Instance.PlaySound(SoundManager.SoundType.HeartTap);
        }
    }

    public void CloseMenu()
    {
        menuBase.SetActive(false);
        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);
    }

    public void BuyUnit(string str)
    {
        int price = 0;
        EntityType entityType = (EntityType)System.Enum.Parse(typeof(EntityType), str);

        //method returns price by reference
        prices.TryGetValue(entityType, out price);
        if (money >= price)
        {
            money -= price;
            textMoneyMenu.text = $"{money}";
            textMoneyBase.text = $"{money}";

            ActionTimer actionTimer = new GameObject(entityType.ToString() + "Timer").AddComponent<ActionTimer>();
            actionTimer.Timer = sceneManager.TimeToSpawn[entityType];
            actionTimer.SomeAction = (() => sceneManager.SpawnEntity(entityType));
            actionTimer.SomeAction = (() => Destroy(actionTimer.gameObject));
            actionTimer.SomeAction = (() => SoundManager.Instance.PlaySound(SoundManager.SoundType.UnitSpawn));
        }

        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);
    }

    public void ShowInfoUnit(string str)
    {
        unitInfo.SetActive(true);

        //Show information about units
        EntityType unitType = (EntityType)System.Enum.Parse(typeof(EntityType), str);
        string temp;
        if (unitType == EntityType.Granulocyte) temp = $" � ������������";
        else if (unitType == EntityType.Lymphocyte) temp = $" � ����������";
        else if (unitType == EntityType.Erythrocyte) temp = $" �� �����������";
        else temp = $" ERROR";

        textInfo.text = $"Here will be some information about " + temp;

        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);
    }

    public void CloseInfoUnit()
    {
        textInfo.text = "";
        unitInfo.SetActive(false);

        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);
    }

    public void PauseButton()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);
        //Today we do not have job for this button
        Debug.Log("Clicking on pause!\n");
    }

    public void ShowBaseButton()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);
        //Today we do not have job for this button
        Debug.Log("Showing Base!\n");
    }
    ///////         Public methods, called from buttons         ///////

    public void IncreaseMoney()
    {
        ++money;
        textMoneyMenu.text = $"{money}";
        textMoneyBase.text = $"{money}";
    }

    //TO DO: call it from real end game (when base is dead or enemies are killed)
    private void EndLevel(EndGameEnum res)
    {
        //remember scale
        scale = Time.timeScale;
        Time.timeScale = 0;

        menuEndLevel.SetActive(true);

        if (res == EndGameEnum.WIN)
        {
            //set this level cleared and make firstNotCleared level = next level
            Loader.LoaderInstance.SetLevelCleared(Loader.LoaderInstance.CurrentLevel);
            Loader.LoaderInstance.CurrentLevel = Loader.LoaderInstance.FirstNotClearedLevel;

            SceneManager.sceneManager.InitLevel();
            textResultLevel.text = $"You won!";
            textLoadLevel.text = $"Next\nlevel";
        }
        else //lose
        {
            textLoadLevel.text = $"Again";
            textResultLevel.text = $"You lost...";
        }
    }

    ////////// Methods from EndGameMenu //////////
    public void LoadLevelButton()
    {
        Time.timeScale = scale;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = scale;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    //////////

    private void ChangeForceTextAndCloseMenuIfNeeded()
    {
        textForceBase.text = force < 0 ? $"{0}" : $"{force}";
        if (force <= 0)
        {
            CloseMenu();
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        //Init();
        
        base.Start();
        textMoneyMenu.text = $"{money}";
        textMoneyBase.text = $"{money}";
        textForceBase.text = $"{force}";

        //add price for units to screen
        int price;
        //method returns price by reference
        prices.TryGetValue(EntityType.Erythrocyte, out price); textEritro.text += $" {price}";
        prices.TryGetValue(EntityType.Granulocyte, out price); textGranulo.text += $" {price}";
        prices.TryGetValue(EntityType.Lymphocyte, out price); textLimfo.text += $" {price}";

        menuBase.SetActive(false);
        unitInfo.SetActive(false);
        menuEndLevel.SetActive(false);

        entityTakeDamageEvent += ChangeForceTextAndCloseMenuIfNeeded;
        entityTakeDamageEvent += (() => SoundManager.Instance.PlaySound(SoundManager.SoundType.HeartDamage));
        entityTakeDamageEvent += (() => { if (force <= 0) SoundManager.Instance.PlaySound(SoundManager.SoundType.HeartDead); });
    }

    private enum EndGameEnum
    {
        WIN,
        LOSE
    }
}