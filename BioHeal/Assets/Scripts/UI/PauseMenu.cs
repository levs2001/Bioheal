using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private const float BACKGROUND_TRANSPARENCY = 0.7f;
    
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private Button resumeGameButton;
    [SerializeField] private Button howToPlayButton;
    [SerializeField] private Button goToMainMenuButton;
    [SerializeField] private Button retryButton;

    [SerializeField] private GameObject howToPlay;
    [SerializeField] private GameObject background;
    

    private float scale; //field to remember timeScale

    public void PauseButton()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);

        pauseMenu.SetActive(true);
        background.SetActive(true);
        scale = Time.timeScale;
        Time.timeScale = 0;

        //all buttons are not available except buttons at PauseMenu
        Object[] buttons = GameObject.FindObjectsOfType(typeof(Button));
        foreach (Button b in buttons)
        {
            b.GetComponent<Button>().interactable = false;
        }
        resumeGameButton.GetComponent<Button>().interactable = true;
        howToPlayButton.GetComponent<Button>().interactable = true;
        goToMainMenuButton.GetComponent<Button>().interactable = true;
        retryButton.GetComponent<Button>().interactable = true;
    }

    public void ResumeGame()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);

        Object[] buttons = GameObject.FindObjectsOfType(typeof(Button));
        foreach (Button b in buttons)
        {
            b.GetComponent<Button>().interactable = true;
        }

        Time.timeScale = scale;
        background.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void OpenHowToPlay()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);
        howToPlay.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);
        howToPlay.SetActive(false);
    }

    public void Retry()
    {
        Time.timeScale = scale;
        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);
        UnityEngine.SceneManagement.SceneManager.LoadScene(Loader.GAME_SCENE);
    }

    public void GoToMainMenu()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);
        Time.timeScale = scale;
        UnityEngine.SceneManagement.SceneManager.LoadScene(Loader.MAIN_MENU_SCENE);

        SoundManager.Instance.StopSound(SoundManager.SoundType.MainTheme);
    }

    // Start is called before the first frame update
    private void Start()
    {
        howToPlay.SetActive(false);
        pauseMenu.SetActive(false);
        background.SetActive(false);

        var backgroundImage = background.GetComponent<Image>();
        var tempColor = backgroundImage.color;
        tempColor.a = BACKGROUND_TRANSPARENCY;
        backgroundImage.color = tempColor;
    }
}
