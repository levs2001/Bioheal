using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public enum MenuChapterType
    {
        Settings,
        HowToPlay,
        ChooseLevel
    }

    public class MenuChapter
    {
        private GameObject image;

        //C# does not let create local instances using constructor with 
        //argument as another local instance. So we need initialize this object after creating it
        public void Init(GameObject _image) { image = _image; }

        public void OpenChapter()
        {
            image.SetActive(true);
        }
        public void CloseChapter()
        {
            image.SetActive(false);
        }
    }

    [SerializeField] private GameObject settingsImage;
    [SerializeField] private GameObject howToPlayImage;
    [SerializeField] private GameObject chooseLevelImage;

    public MenuChapter chooseLevelChapter = new MenuChapter();
    public MenuChapter howToPlayChapter = new MenuChapter();
    public MenuChapter settingsChapter = new MenuChapter();

    private Dictionary<MenuChapterType, MenuChapter> chapters = new Dictionary<MenuChapterType, MenuChapter>();

    public void OpenChapter(string str)
    {
        MenuChapter chapter;
        MenuChapterType chapterType = (MenuChapterType)System.Enum.Parse(typeof(MenuChapterType), str);
        chapters.TryGetValue(chapterType, out chapter);

        chapter.OpenChapter();
    }

    public void CloseChapter(string str)
    {
        MenuChapter chapter;
        MenuChapterType chapterType = (MenuChapterType)System.Enum.Parse(typeof(MenuChapterType), str);
        chapters.TryGetValue(chapterType, out chapter);

        chapter.CloseChapter();
    }

    public void LoadGameScene()
    {
        Loader.LoadConfig();
        // print(Loader.GetConfig().levels[0].mineral.initialC);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        //It does not work in editor, only at game's runtime 
        Application.Quit();
    }


    // Start is called before the first frame update
    void Start()
    {
        settingsImage.SetActive(false);
        settingsImage.SetActive(false);
        howToPlayImage.SetActive(false);
        chooseLevelImage.SetActive(false);

        chooseLevelChapter.Init(chooseLevelImage);
        howToPlayChapter.Init(howToPlayImage);
        settingsChapter.Init(settingsImage);

        chapters.Add(MenuChapterType.ChooseLevel, chooseLevelChapter);
        chapters.Add(MenuChapterType.HowToPlay, howToPlayChapter);
        chapters.Add(MenuChapterType.Settings, settingsChapter);
    }

    // Update is called once per frame
    void Update()
    {

    }
}