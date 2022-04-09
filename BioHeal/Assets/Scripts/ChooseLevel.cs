using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Loader;

public class ChooseLevel : MonoBehaviour
{
    //TODO: read it from Loader
    private int levels_per_page = 15, first_page = 1;
    private int countOpenedLevel, page, last_page;
    private long amountOfLevels;

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
        countOpenedLevel = Loader.LoaderInstance.FirstNotClearedLevel + 1;
        amountOfLevels = Loader.LoaderInstance.AmountOfLevels;

        last_page = (int)Math.Ceiling((double)amountOfLevels / levels_per_page);

        page = first_page;
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).GetComponent<Button>().enabled = true;
            transform.GetChild(i).GetComponent<Image>().enabled = true;
            transform.GetChild(i).GetComponentInChildren<Text>().enabled = true;


            //if level is open
            if ((i + 1) + (levels_per_page * (page - 1)) <= countOpenedLevel)
            {
                transform.GetChild(i).GetComponent<Image>().sprite = openedIcon;
                transform.GetChild(i).GetComponent<Button>().interactable = true;
                transform.GetChild(i).GetComponentInChildren<Text>().text = $"{(i + 1) + (levels_per_page * (page - 1))}";
            }

            //level exists but closed
            else if ((i + 1) + (levels_per_page * (page - 1)) > countOpenedLevel && (i + 1) + (levels_per_page * (page - 1)) <= amountOfLevels)
            {
                transform.GetChild(i).GetComponent<Image>().sprite = closedIcon;
                transform.GetChild(i).GetComponent<Button>().interactable = false;
                transform.GetChild(i).GetComponentInChildren<Text>().text = $"{(i + 1) + (levels_per_page * (page - 1))}";
            }

            //level does not exist
            else if ((i + 1) + (levels_per_page * (page - 1)) > countOpenedLevel && (i + 1) + (levels_per_page * (page - 1)) > amountOfLevels)
            {
                transform.GetChild(i).GetComponent<Button>().enabled = false;
                transform.GetChild(i).GetComponent<Image>().enabled = false;
                transform.GetChild(i).GetComponentInChildren<Text>().enabled = false;
            }
        }
    }
}
