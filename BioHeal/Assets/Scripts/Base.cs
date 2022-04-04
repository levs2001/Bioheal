using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SceneManager;

public class Base : Alive
{
    //SerializeField, because I am initializing these fields from Unity API from inspector 
    [SerializeField] private GameObject menuBase;
    [SerializeField] private Text textMoneyMenu;
    [SerializeField] private Text textLimfo, textGranulo, textEritro;
    [SerializeField] private GameObject unitInfo;
    [SerializeField] private Text textInfo;
    [SerializeField] private Text textMoneyBase, textForceBase;

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
        menuBase.SetActive(true);
    }

    public void CloseMenu()
    {
        menuBase.SetActive(false);
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
        }
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
    }

    public void CloseInfoUnit()
    {
        textInfo.text = "";
        unitInfo.SetActive(false);
    }

    public void PauseButton()
    {
        //Today we do not have job for this button
        Debug.Log("Clicking on pause!\n");
    }

    public void ShowBaseButton()
    {
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

    private void ChangeForceTextAndCloseMenuIfNeeded()
    {
        textForceBase.text = force < 0 ? $"{0}" : $"{force}";
        if (force <= 0)
        {
            CloseMenu();
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        //Init();

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

        entityTakeDamageEvent += ChangeForceTextAndCloseMenuIfNeeded;
    }
}