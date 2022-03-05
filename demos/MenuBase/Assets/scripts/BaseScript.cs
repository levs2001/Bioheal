using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour
{
    public GameObject obj;
    int money;
    public Text text;
    public Text joke_field;
    string joke = $"Мы оба знаем что за забегаловка, имени какого засранца";

    void Start()
    {
        obj = GameObject.Find("Menu");
        obj.SetActive(false);
        money = 125;
    }

    public void AddMoney()
    {
        money = 125;
        text.text = $"{money}";
        joke_field.text = $"";
    }

    public void OpenMenu()
    {
        obj.SetActive(true);
        text.text = $"{money}";
    }

    public void Buy(int price)
    {
        if (money >= price) {
            money -= price;
            text.text = $"{money}";
            joke_field.text = $"";
        }
        else
            joke_field.text = joke;
        
    }

    public void CloseMenu()
    {
        obj.SetActive(false);
    }
}