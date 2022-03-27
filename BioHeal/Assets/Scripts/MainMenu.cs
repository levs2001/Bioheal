using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsImage;
    [SerializeField] private GameObject howToPlayImage;
    [SerializeField] private GameObject chooseLevelImage;

    public void LoadGameScene()
    {
        Loader.LoadConfig();
        // print(Loader.GetConfig().levels[0].mineral.initialC);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ChooseLevel()
    {
        chooseLevelImage.SetActive(true);
        Debug.Log("Clicking on Choose level!");
    }

    public void ExitChooseLevel()
    {
        chooseLevelImage.SetActive(false);
    }

    public void OpenHowToPlay()
    {
        Debug.Log("Clicking on How to play!");
        howToPlayImage.SetActive(true);
    }

    public void ExitHowToPlay()
    {
        howToPlayImage.SetActive(false);
    }

    public void OpenSettings()
    {
        Debug.Log("Clicking on Settings!");
        settingsImage.SetActive(true);
    }

    public void ExitSettings()
    {
        settingsImage.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Clicking on Exit!");

        //It does not work in editor, only at game's runtime 
        Application.Quit();
    }


    // Start is called before the first frame update
    void Start()
    {
        settingsImage.SetActive(false);
        howToPlayImage.SetActive(false);
        chooseLevelImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
