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
    [SerializeField] private GameObject pauseButton;
    

    [SerializeField] private GameObject howToPlay;
    [SerializeField] private GameObject background;
    

    private float scale; //field to remember timeScale
    private bool isOpened = false;

    private static readonly Log log = LogFactory.GetLog(typeof(PauseMenu));

    private static PauseMenu instance = null;
    public static PauseMenu Instance
    {
        get
        {
            if (instance == null)
            {
                log.Error(new System.Exception("PauseMenu does not exist"));
            }

            return instance;
        }
    }

    public void EscapeButton()
    {
        if (!isOpened)
        {
            PauseButton();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseButton()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);

        pauseMenu.SetActive(true);
        background.SetActive(true);
        pauseButton.SetActive(false); // resume game button is displayed instead

        scale = Time.timeScale;
        Time.timeScale = 0;
        isOpened = true;

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
        isOpened = false;
        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);

        Object[] buttons = GameObject.FindObjectsOfType(typeof(Button));
        foreach (Button b in buttons)
        {
            b.GetComponent<Button>().interactable = true;
        }

        Time.timeScale = scale;
        background.SetActive(false);
        pauseMenu.SetActive(false);
        pauseButton.SetActive(false);
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
        isOpened = false;
        Time.timeScale = scale;
        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);
        UnityEngine.SceneManagement.SceneManager.LoadScene(Loader.GAME_SCENE);
    }

    public void GoToMainMenu()
    {
        isOpened = false;
        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);
        Time.timeScale = scale;
        UnityEngine.SceneManagement.SceneManager.LoadScene(Loader.MAIN_MENU_SCENE);

        SoundManager.Instance.StopSound(SoundManager.SoundType.MainTheme);
    }

    // Start is called before the first frame update
    private void Start()
    {
        instance = this;
        howToPlay.SetActive(false);
        pauseMenu.SetActive(false);
        background.SetActive(false);

        var backgroundImage = background.GetComponent<Image>();
        var tempColor = backgroundImage.color;
        tempColor.a = BACKGROUND_TRANSPARENCY;
        backgroundImage.color = tempColor;
    }
}
