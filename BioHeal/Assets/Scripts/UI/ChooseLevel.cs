using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Loader;

public class ChooseLevel : MonoBehaviour
{
    private const int levels_per_page = 15, first_page = 0;
    private int page, last_page;
    private long amountOfLevels, countOpenedLevel;

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
        int level = (int)Int32.Parse(button.GetComponentInChildren<Text>().text) - 1;
        Debug.Log($"Load level #{level + 1}");

        Loader.LoaderInstance.CurrentLevel = level;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    // Start is called before the first frame update
    private void Start()
    {
        //all levels are cleared
        if(Loader.LoaderInstance.AreAllLevelsCleared())
            countOpenedLevel = Loader.LoaderInstance.AmountOfLevels;
        else
            countOpenedLevel = Loader.LoaderInstance.FirstNotClearedLevel;

        amountOfLevels = Loader.LoaderInstance.AmountOfLevels;

        last_page = (int)Math.Floor((double)amountOfLevels / levels_per_page);

        page = first_page;
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        for (int butNum = 0; butNum < transform.childCount; ++butNum)
        {
            transform.GetChild(butNum).GetComponent<Button>().enabled = true;
            transform.GetChild(butNum).GetComponent<Image>().enabled = true;
            transform.GetChild(butNum).GetComponentInChildren<Text>().enabled = true;

            //if level is open
            if (butNum + levels_per_page * page <= countOpenedLevel)
            {
                transform.GetChild(butNum).GetComponent<Image>().sprite = openedIcon;
                transform.GetChild(butNum).GetComponent<Button>().interactable = true;
                transform.GetChild(butNum).GetComponentInChildren<Text>().text = $"{(butNum + 1) + (levels_per_page * page)}";
            }

            //level exists but closed
            else if (butNum + levels_per_page * page <= amountOfLevels)
            {
                transform.GetChild(butNum).GetComponent<Image>().sprite = closedIcon;
                transform.GetChild(butNum).GetComponent<Button>().interactable = false;
                transform.GetChild(butNum).GetComponentInChildren<Text>().text = $"{(butNum + 1) + (levels_per_page * page)}";
            }

            //level does not exist
            else
            {
                transform.GetChild(butNum).GetComponent<Button>().enabled = false;
                transform.GetChild(butNum).GetComponent<Image>().enabled = false;
                transform.GetChild(butNum).GetComponentInChildren<Text>().enabled = false;
            }
        }
    }
}
