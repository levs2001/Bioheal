using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour
{
    private const int first_page = 0, last_page = 2;
    private int page = first_page;

    [SerializeField] private Canvas[] arrCanvas = new Canvas[last_page + 1];

    private static HowToPlay instMenuScene = null;
    private static HowToPlay instGameScene = null;

    public static HowToPlay InstanceMenuScene
    {
        get
        {
            //it is initialized at Awake()
            if (instMenuScene == null)
                throw new System.Exception("HowToPlay does not exist");
            return instMenuScene;
        }
    }

    public static HowToPlay InstanceGameScene
    {
        get
        {
            //it is initialized at Awake()
            if (instGameScene == null)
                throw new System.Exception("HowToPlay does not exist");
            return instGameScene;
        }
    }

    public void SetFirstPage()
    {
        page = first_page;
        foreach (Canvas c in arrCanvas)
        {
            c.gameObject.SetActive(false);
        }
        arrCanvas[0].gameObject.SetActive(true);
    }

    public void NextPage()
    {
        if (page < last_page)
        {
            arrCanvas[page].gameObject.SetActive(false);
            ++page;
            arrCanvas[page].gameObject.SetActive(true);
        }
    }

    public void PrevPage()
    {
        if (page > first_page)
        {
            arrCanvas[page].gameObject.SetActive(false);
            --page;
            arrCanvas[page].gameObject.SetActive(true);
        }
    }

    private void Awake()
    {

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MenuScene")
        {
            if (instMenuScene == null)
            {
                instMenuScene = this;
                DontDestroyOnLoad(gameObject);

                SetFirstPage();
            }
            else if (instMenuScene != this)
            {
                Destroy(gameObject);
            }
        }

        else if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "GameScene")
        {
            if (instGameScene == null)
            {
                instGameScene = this;
                DontDestroyOnLoad(gameObject);

                SetFirstPage();
            }
            else if (instGameScene != this)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
