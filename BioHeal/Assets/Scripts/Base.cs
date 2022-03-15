using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    private uint force, money;
    private uint eritroPrice, granuloPrice, limfoPrice;

    //public fields, because I am initializing these fields from Unity API from inspector 
    public GameObject menuBase;
    public Text textMoney;
    public Text textLimfo, textGranulo, textEritro;
    public GameObject unitInfo;
    public Text textInfo;

    private void Init()
    {
        //init
        money = 375;

        //init. Later - read from json
        force = 0;
        eritroPrice = 10;
        granuloPrice = 15;
        limfoPrice = 20;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();

        textMoney.text = $"{money}";

        //add price for units to screen
        textEritro.text += $" {eritroPrice}";
        textGranulo.text += $" {granuloPrice}";
        textLimfo.text += $" {limfoPrice}";

        menuBase.SetActive(false);
        unitInfo.SetActive(false);
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

    public void BuyEritro()
    {
        if (money >= eritroPrice)
        {
            money -= eritroPrice;
            textMoney.text = $"{money}";
            //ask manager to spawn granulocits
        }
    }

    public void BuyGranulo()
    {
        if (money >= granuloPrice)
        {
            money -= granuloPrice;
            textMoney.text = $"{money}";
            //ask manager to spawn granulocits
        }
    }

    public void BuyLimfo()
    { 
        if (money >= limfoPrice)
        {
            money -= limfoPrice;
            textMoney.text = $"{money}";
            //ask manager to spawn limfocits
        }
    }

    //unit == "InfoLimfo" or "InfoGranulo" or "InfoEritro"
    public void ShowInfoUnit(string unit)
    {
        unitInfo.SetActive(true);

        //Вывод информации о юнитах. Пока вывожу костыль
        string temp;
        if (unit == "granulo") temp = $" о гранулоцитах";
        else if (unit == "limfo") temp = $" о лимфоцитах";
        else if (unit == "eritro") temp = $" об эритроцитах";
        else temp = $" ERROR";

        textInfo.text = $"Здесь будет информация" + temp;
    }

    public void CloseInfoUnit()
    {
        textInfo.text = "";
        unitInfo.SetActive(false); 
    }
    ///////         Public methods, called from buttons         ///////


    public void IncreaseMoney() { ++money; }
}
