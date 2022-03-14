using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    private uint force, money;
    private uint eritroPrice, granuloPrice, limfoPrice;

    public GameObject menuBase;

    public Text textMoney;
    public Text textLimfo, textGranulo, textEritro;

    public GameObject unitInfo;
    public Text textInfo;

    // Start is called before the first frame update
    void Start(){

        //init
        money = 375;
        if (textMoney != null)
            textMoney.text = $"{money}";

        //init. Later - read from json
        force = 0;
        eritroPrice = 10;
        granuloPrice = 15;
        limfoPrice = 20;

        //add price for units to screen
        textEritro.text += $" {eritroPrice}";
        textGranulo.text += $" {granuloPrice}";
        textLimfo.text += $" {limfoPrice}";

        if (menuBase != null) {
            menuBase.SetActive(false);
        }

        if (unitInfo != null) {
            unitInfo.SetActive(false);
            textInfo.fontSize = 16;
        }
    }

    ///////         Public methods, called from buttons         ///////
    public void OpenMenu() {
        if (menuBase != null)
            menuBase.SetActive(true);
    }

    public void CloseMenu() {
        if (menuBase != null)
            menuBase.SetActive(false);
    }


    public void BuyEritro(){
        if (money >= eritroPrice){
            money -= eritroPrice;
            textMoney.text = $"{money}";
            //ask manager to spawn granulocits
        }

    }
    public void BuyGranulo(){
        if (money >= granuloPrice) {
            money -= granuloPrice;
            textMoney.text = $"{money}";
            //ask manager to spawn granulocits
        }
    }
    public void BuyLimfo(){

        if (money >= limfoPrice){
            money -= limfoPrice;
            textMoney.text = $"{money}";
            //ask manager to spawn limfocits
        }
    }

    //unit == "InfoLimfo" or "InfoGranulo" or "InfoEritro"
    public void ShowInfoUnit(string unit){
        if (unitInfo == null)
            return;
        unitInfo.SetActive(true);

        //Вывод информации о юнитах. Пока вывожу костыль
        string temp;
        if (unit == "granulo") temp = $" о гранулоцитах";
        else if (unit == "limfo") temp = $" о лимфоцитах";
        else if (unit == "eritro") temp = $" об эритроцитах";
        else temp = $" ERROR";

        textInfo.text = $"Здесь будет информация" + temp;
    }

    public void CloseInfoUnit(){
        if (unitInfo != null) {
            textInfo.text = "";
            unitInfo.SetActive(false);
        }
    }
    //////////////Public methods, called from buttons


    public void IncreaseMoney() { ++money; }

}
