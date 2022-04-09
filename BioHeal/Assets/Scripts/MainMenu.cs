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
        Loader.LoaderInstance.CurrentLevel = Loader.LoaderInstance.FirstNotClearedLevel;
        Debug.Log($"Load level #{Loader.LoaderInstance.CurrentLevel + 1}");
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
            SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);
        }

        public void CloseChapter()
        {
            image.SetActive(false);
            SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);
        }
    }

    private enum MenuChapterType
    {
        Settings,
        HowToPlay,
        ChooseLevel
    }
}