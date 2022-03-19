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

    private uint force, money;
    private Dictionary<EntityType, uint> dict = new Dictionary<EntityType, uint>();


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
        uint price = 0;
        EntityType entityType = (EntityType)System.Enum.Parse(typeof(EntityType), str);

        //method returns price by reference
        dict.TryGetValue(entityType, out price);
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
        Init();

        textMoney.text = $"{money}";

        //add price for units to screen
        uint price;
        //method returns price by reference
        dict.TryGetValue(EntityType.Erythrocyte, out price); textEritro.text += $" {price}";
        dict.TryGetValue(EntityType.Granulocyte, out price); textGranulo.text += $" {price}";
        dict.TryGetValue(EntityType.Lymfocyte, out price); textLimfo.text += $" {price}";

        menuBase.SetActive(false);
        unitInfo.SetActive(false);
    }

    private void Init()
    {
        //init
        money = 375;

        //init. Later - read from json
        dict.Add(EntityType.Erythrocyte, 10);
        dict.Add(EntityType.Granulocyte, 15);
        dict.Add(EntityType.Lymfocyte, 20);

        force = 0;
    }
}