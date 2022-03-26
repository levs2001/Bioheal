using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SceneManager;

public class Base : MonoBehaviour
{
<<<<<<< Updated upstream
    //public fields, because I am initializing these fields from Unity API from inspector 
    public GameObject menuBase;
    public Text textMoney;
    public Text textLimfo, textGranulo, textEritro;
    public GameObject unitInfo;
    public Text textInfo;
=======
    //SerializeField, because I am initializing these fields from Unity API from inspector 
    [SerializeField] private GameObject menuBase;
    [SerializeField] private Text textMoneyMenu;
    [SerializeField] private Text textLimfo, textGranulo, textEritro;
    [SerializeField] private GameObject unitInfo;
    [SerializeField] private Text textInfo;
    [SerializeField] private Text textMoneyBase, textForceBase;
>>>>>>> Stashed changes

    private int force, money;
    private Dictionary<EntityType, int> prices = new Dictionary<EntityType, int>();

    public int Force
    {
        set { force = value; }
    }

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
            sceneManager.SpawnEntity(entityType);
        }
    }

    public void ShowInfoUnit(string str)
    {
        unitInfo.SetActive(true);

        //Show information about units
        EntityType unitType = (EntityType)System.Enum.Parse(typeof(EntityType), str);
        string temp;
<<<<<<< Updated upstream
        if (unitType == EntityType.Granulocit) temp = $" о гранулоцитах";
        else if (unitType == EntityType.Lymfocyte) temp = $" о лимфоцитах";
        else if (unitType == EntityType.Erythrocyte) temp = $" об эритроцитах";
        else temp = $" ERROR";

        textInfo.text = $"Здесь будет информация" + temp;
=======
        if (unitType == EntityType.Granulocyte) temp = $"granulocyte";
        else if (unitType == EntityType.Lymfocyte) temp = $"lymfocyte";
        else if (unitType == EntityType.Erythrocyte) temp = $"erythrocyte";
        else temp = $" ERROR";

        textInfo.text = $"Here will be some information about " + temp;
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======
        textMoneyMenu.text = $"{money}";
        textMoneyBase.text = $"{money}";
>>>>>>> Stashed changes
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
        prices.TryGetValue(EntityType.Lymfocyte, out price); textLimfo.text += $" {price}";

        menuBase.SetActive(false);
        unitInfo.SetActive(false);
    }
}