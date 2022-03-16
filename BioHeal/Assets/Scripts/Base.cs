using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    public enum EntityType
    {
        Erythrocyte,
        Lymfocyte,
        Granulocit,
    }

    private uint force, money;

    private Dictionary<EntityType, uint> dict = new Dictionary<EntityType, uint>();

    //public fields, because I am initializing these fields from Unity API from inspector 
    public GameObject menuBase;
    public Text textMoney;
    public Text textLimfo, textGranulo, textEritro;
    public GameObject unitInfo;
    public Text textInfo;

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
        EntityType unitType = (EntityType)System.Enum.Parse(typeof(EntityType), str);

        //method returns price by reference
        dict.TryGetValue(unitType, out price);
        if (money >= price)
        {
            money -= price;
            textMoney.text = $"{money}";
            //ask manager to spawn [unitType] unit
        }
    }

    public void ShowInfoUnit(string str)
    {
        unitInfo.SetActive(true);

        //Show information about units
        EntityType unitType = (EntityType)System.Enum.Parse(typeof(EntityType), str);
        string temp;
        if (unitType == EntityType.Granulocit) temp = $" о гранулоцитах";
        else if (unitType == EntityType.Lymfocyte) temp = $" о лимфоцитах";
        else if (unitType == EntityType.Erythrocyte) temp = $" об эритроцитах";
        else temp = $" ERROR";

        textInfo.text = $"Здесь будет информация" + temp;
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
        dict.TryGetValue(EntityType.Granulocit, out price); textGranulo.text += $" {price}";
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
        dict.Add(EntityType.Granulocit, 15);
        dict.Add(EntityType.Lymfocyte, 20);

        force = 0;
    }
}