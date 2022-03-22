using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SceneManager;

public class Base : MonoBehaviour
{
    //public fields, because I am initializing these fields from Unity API from inspector 
    [SerializeField] private GameObject menuBase;
    [SerializeField] private Text textMoney;
    [SerializeField] private Text textLimfo, textGranulo, textEritro;
    [SerializeField] private GameObject unitInfo;
    [SerializeField] private Text textInfo;

    private int force, money;
    private Dictionary<EntityType, int> prices = new Dictionary<EntityType, int>();

    public int Force
    {
        set
        {
            force = value;
        }
    }
    public int Money
    {
        set
        {
            money = value;
        }
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
            textMoney.text = $"{money}";
            sceneManager.SpawnEntity(entityType);
        }
    }

    public void ShowInfoUnit(string str)
    {
        unitInfo.SetActive(true);

        //Show information about units
        EntityType unitType = (EntityType)System.Enum.Parse(typeof(EntityType), str);
        string temp;
        if (unitType == EntityType.Granulocyte) temp = $" � ������������";
        else if (unitType == EntityType.Lymfocyte) temp = $" � ����������";
        else if (unitType == EntityType.Erythrocyte) temp = $" �� �����������";
        else temp = $" ERROR";

        textInfo.text = $"����� ����� ����������" + temp;
    }

    public void CloseInfoUnit()
    {
        textInfo.text = "";
        unitInfo.SetActive(false);
    }
    ///////         Public methods, called from buttons         ///////

    public void IncreaseMoney()
    {
        ++money;
        textMoney.text = $"{money}";
    }

    // Start is called before the first frame update
    private void Start()
    {
        //Init();

        textMoney.text = $"{money}";

        //add price for units to screen
        int price;
        //method returns price by reference
        prices.TryGetValue(EntityType.Erythrocyte, out price); textEritro.text += $" {price}";
        prices.TryGetValue(EntityType.Granulocyte, out price); textGranulo.text += $" {price}";
        prices.TryGetValue(EntityType.Lymfocyte, out price); textLimfo.text += $" {price}";

        menuBase.SetActive(false);
        unitInfo.SetActive(false);
    }
    public void SetPrices(Dictionary<EntityType, int> prices)
    {
        this.prices = prices;
    }
    //private void Init(InitHeartData data)
    //{
    //    //init
    //    money = 375;

    //    //init. Later - read from json
    //    prices.Add(EntityType.Erythrocyte, 10);
    //    prices.Add(EntityType.Granulocyte, 15);
    //    prices.Add(EntityType.Lymfocyte, 20);

    //    force = 0;
    //}

    //public class InitHeartData
    //{
    //    public int money = 300;
    //    public int force;
    //    public Dictionary<EntityType, uint> prices = new Dictionary<EntityType, uint>();

    //}
}