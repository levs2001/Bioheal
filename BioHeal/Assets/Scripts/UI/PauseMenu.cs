using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private readonly static Color32 LOSE_BACKGROUND_COLOR = new Color32(200,200,200,200);
    private readonly static Color32 PAUSE_BACKGROUND_COLOR = new Color32(255,255,255,200);
    private readonly static Color32 WIN_BACKGROUND_COLOR = PAUSE_BACKGROUND_COLOR;
    
    // private PauseMenuState state = PauseMenuState.Pause;
    
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private Text pauseMenuText;
    [SerializeField] private GameObject resumeGameButton;
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
        if (pauseMenuText.text.Equals($"pause")) // shouldnt work in win/lose menus 
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
    }

    public void PauseButton()
    {
        pauseMenuText.text = $"pause";
        background.GetComponent<Image>().color = PAUSE_BACKGROUND_COLOR;
        MenuActions();
    }

    private void MenuActions()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);

        pauseMenu.SetActive(true);
        background.SetActive(true);
        resumeGameButton.SetActive(true);

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
        howToPlayButton.GetComponent<Button>().interactable = true;
        goToMainMenuButton.GetComponent<Button>().interactable = true;
        retryButton.GetComponent<Button>().interactable = true;

        resumeGameButton.GetComponent<Button>().interactable = true;
    }

    public void OpenLoseLevelMenu() 
    {
        pauseMenuText.text = $"you lost!";
        background.GetComponent<Image>().color = LOSE_BACKGROUND_COLOR;
        MenuActions();
        resumeGameButton.SetActive(false); // both resume/goNext and pause buttons are disabled
    }

     public void OpenWinLevelMenu() 
    {
        pauseMenuText.text = $"you won!";
        background.GetComponent<Image>().color = WIN_BACKGROUND_COLOR;
        MenuActions();
        
        //set this level cleared
        Loader.LoaderInstance.SetLevelCleared(Loader.LoaderInstance.CurrentLevel);

        if (Loader.LoaderInstance.IsItLastLevel() && Loader.LoaderInstance.AreAllLevelsCleared())
        {
            //if this was last level - cant go next level
            resumeGameButton.SetActive(false);
        }
        else
        {
            //load next level if this was not last level
            ++Loader.LoaderInstance.CurrentLevel;
        }
    }

    // resumes game in pause menu as well as loads next level in win menu
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
        pauseButton.SetActive(true);
        resumeGameButton.SetActive(false);

        if (pauseMenuText.text.Equals("you won!")) // if we're in win menu
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(Loader.GAME_SCENE);
        }
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
        Loader.LoaderInstance.CurrentLevel--;
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
        pauseButton.SetActive(true);
        resumeGameButton.SetActive(false);
        pauseMenuText.text = $"pause";

        background.GetComponent<Image>().color = PAUSE_BACKGROUND_COLOR;
    }
}
