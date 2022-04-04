using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsImage;
    [SerializeField] private GameObject howToPlayImage;
    [SerializeField] private GameObject chooseLevelImage;

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
    private void Start()
    {
        chapters.Add(MenuChapterType.ChooseLevel, new MenuChapter(chooseLevelImage));
        chapters.Add(MenuChapterType.HowToPlay, new MenuChapter(howToPlayImage));
        chapters.Add(MenuChapterType.Settings, new MenuChapter(settingsImage));
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private class MenuChapter
    {
        private GameObject image;

        public MenuChapter(GameObject image)
        {
            this.image = image;
            this.image.SetActive(false);
        }

        public void OpenChapter()
        {
            image.SetActive(true);
        }

        public void CloseChapter()
        {
            image.SetActive(false);
        }
    }

    private enum MenuChapterType
    {
        Settings,
        HowToPlay,
        ChooseLevel
    }
}