using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private readonly static Color32 LOSE_BACKGROUND_COLOR = new Color32(236,143,175,200);
    private readonly static Color32 PAUSE_BACKGROUND_COLOR = new Color32(255,173,200,200);
    private readonly static Color32 WIN_BACKGROUND_COLOR = PAUSE_BACKGROUND_COLOR;
        
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private Text pauseMenuText;
    [SerializeField] private GameObject resumeGameButton;
    [SerializeField] private Button howToPlayButton;
    [SerializeField] private Button goToMainMenuButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject settingsButton;

    [SerializeField] private GameObject howToPlay;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject background;

    [SerializeField] private GameObject pauseTextImage;
    [SerializeField] private GameObject winTextImage;
    [SerializeField] private GameObject loseTextImage;
    private Dictionary<ActiveTextImage, GameObject> menuTextImages = new Dictionary<ActiveTextImage, GameObject>();
    private ActiveTextImage currenlyActive;
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
        if (currenlyActive.Equals(ActiveTextImage.PAUSE_TEXT)) // shouldnt work in win/lose menus 
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
        SetActiveTextImage(ActiveTextImage.PAUSE_TEXT);
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
        settingsButton.GetComponent<Button>().interactable = true;

        resumeGameButton.GetComponent<Button>().interactable = true;
    }

    public void OpenLoseLevelMenu() 
    {
        SetActiveTextImage(ActiveTextImage.LOST_TEXT);
        background.GetComponent<Image>().color = LOSE_BACKGROUND_COLOR;
        MenuActions();
        resumeGameButton.SetActive(false); // both resume/goNext and pause buttons are disabled
    }

     public void OpenWinLevelMenu() 
    {
        SetActiveTextImage(ActiveTextImage.WON_TEXT);
        background.GetComponent<Image>().color = WIN_BACKGROUND_COLOR;
        MenuActions();
        
        //set this level cleared
        Loader.LoaderInstance.SetLevelCleared(Loader.LoaderInstance.CurrentLevel);

        if (Loader.LoaderInstance.IsItLastLevel() && Loader.LoaderInstance.AreAllLevelsCleared())
        {
            //if this was last level - cant go next level
            resumeGameButton.SetActive(false);
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

        if (currenlyActive.Equals(ActiveTextImage.WON_TEXT)) // if we're in win menu
        {
            ++Loader.LoaderInstance.CurrentLevel;
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
        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);
        UnityEngine.SceneManagement.SceneManager.LoadScene(Loader.GAME_SCENE);
    }

    public void OpenSettings()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);
        settings.SetActive(true);
    }

    public void CloseSettings()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);
        settings.SetActive(false);
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
        settings.SetActive(false);
        pauseMenu.SetActive(false);
        background.SetActive(false);
        pauseButton.SetActive(true);
        resumeGameButton.SetActive(false);
        InitTextImages();
        SetActiveTextImage(ActiveTextImage.PAUSE_TEXT);

        background.GetComponent<Image>().color = PAUSE_BACKGROUND_COLOR;
    }

    private void InitTextImages()
    {
        menuTextImages[ActiveTextImage.PAUSE_TEXT] = pauseTextImage;
        menuTextImages[ActiveTextImage.WON_TEXT] = winTextImage;
        menuTextImages[ActiveTextImage.LOST_TEXT] = loseTextImage;
        foreach (var textImage in menuTextImages)
        {
            menuTextImages[textImage.Key].SetActive(false);
        }
    }

    private void SetActiveTextImage(ActiveTextImage newActivetextImage)
    {  
        menuTextImages[currenlyActive].SetActive(false);
        menuTextImages[newActivetextImage].SetActive(true);   
        currenlyActive = newActivetextImage;
    }

    private enum ActiveTextImage
    {
        PAUSE_TEXT = 0,
        WON_TEXT = 1,
        LOST_TEXT = 2  
    } 
}
