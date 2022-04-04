using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevel : MonoBehaviour
{
    //TODO: read it from Loader
    private int countOpenedLevel = 24;
    private int page, levels_per_page = 15, last_page = 4, first_page = 1;

    [SerializeField] private Sprite closedIcon, openedIcon;

    public void OpenNextPage()
    {
        if (page < last_page)
            ++page;
        UpdateButtons();
    }

    public void OpenPrevPage()
    {
        if (page > first_page)
            --page;
        UpdateButtons();
    }

    public void OpenFirstPage()
    {
        page = first_page;
        UpdateButtons();
    }

    public void OpenLastPage()
    {
        page = last_page;
        UpdateButtons();
    }

    public void LoadLevel(Button button)
    {
        int level = (int)Int32.Parse(button.GetComponentInChildren<Text>().text);
        Debug.Log($"Load level #{level}");
    }

    // Start is called before the first frame update
    void Start()
    {
        page = first_page;
        UpdateButtons();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateButtons()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            //if level is open
            if (i + (levels_per_page * (page - 1)) < countOpenedLevel)
            {
                transform.GetChild(i).GetComponent<Image>().sprite = openedIcon;
                transform.GetChild(i).GetComponent<Button>().interactable = true;
            }
            else
            {
                transform.GetChild(i).GetComponent<Image>().sprite = closedIcon;
                transform.GetChild(i).GetComponent<Button>().interactable = false;
            }

            transform.GetChild(i).GetComponentInChildren<Text>().text = $"{(i + 1) + (levels_per_page * (page - 1))}";
        }
    }
}
