using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Loader;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private GameObject menuEndLevel;
    [SerializeField] private Text textLoadLevel;
    [SerializeField] private Text textResultLevel;
    [SerializeField] private Button buttonLoadLevel;
    [SerializeField] private Button buttonGoToMainMenu;

    private float scale; //to remember scale

    private static EndLevel endLevelMenu = null;

    public static EndLevel Instance
    {
        get
        {
            //it is initialized at Awake()
            if (endLevelMenu == null)
                throw new System.Exception("EndLevelMenu not exist");
            return endLevelMenu;
        }
    }

    public void OpenWinLevelMenu()
    {
        scale = Time.timeScale;
        Time.timeScale = 0;

        menuEndLevel.SetActive(true);

        textResultLevel.text = $"You win!";
        textLoadLevel.text = $"Next\nLevel";

        //set this level cleared and make firstNotCleared level = next level
        Loader.LoaderInstance.SetLevelCleared(Loader.LoaderInstance.CurrentLevel);
        Loader.LoaderInstance.CurrentLevel = Loader.LoaderInstance.FirstNotClearedLevel;

        Object[] buttons = GameObject.FindObjectsOfType(typeof(Button));
        foreach (Button b in buttons)
        {
            b.GetComponent<Button>().interactable = false;
        }
        buttonLoadLevel.GetComponent<Button>().interactable = true;
        buttonGoToMainMenu.GetComponent<Button>().interactable = true;
    }

    public void OpenLoseLevelMenu()
    {
        scale = Time.timeScale;
        Time.timeScale = 0;

        menuEndLevel.SetActive(true);

        textResultLevel.text = $"You lost...";
        textLoadLevel.text = $"Again";

        Object[] buttons = GameObject.FindObjectsOfType(typeof(Button));
        foreach (Button b in buttons)
        {
            b.GetComponent<Button>().interactable = false;
        }
        buttonLoadLevel.GetComponent<Button>().interactable = true;
        buttonGoToMainMenu.GetComponent<Button>().interactable = true;
    }

    public void LoadLevelButton()
    {
        Time.timeScale = scale;

        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);

        Object[] buttons = GameObject.FindObjectsOfType(typeof(Button));
        foreach (Button b in buttons)
        {
            b.GetComponent<Button>().interactable = true;
        }

        menuEndLevel.SetActive(false);

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = scale;

        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);

        Object[] buttons = GameObject.FindObjectsOfType(typeof(Button));
        foreach (Button b in buttons)
        {
            b.GetComponent<Button>().interactable = true;
        }
        menuEndLevel.SetActive(false);

        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void Awake()
    {
        if (endLevelMenu == null)
        {
            menuEndLevel.SetActive(false);
            endLevelMenu = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(endLevelMenu != this)
        {
            Destroy(gameObject);
        }
    }

}
