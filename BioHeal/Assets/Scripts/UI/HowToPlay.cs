using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour
{
    private const int first_page = 0, last_page = 2;
    private int page = first_page;

    [SerializeField] private Canvas[] arrCanvas = new Canvas[last_page + 1];

    private static HowToPlay instance = null;

    public static HowToPlay Instance
    {
        get
        {
            //it is initialized at Awake()
            if (instance == null)
            {
                Log log = LogFactory.GetLog(typeof(SoundManager));
                log.Error(new System.Exception("HowToPlay does not exist"));
            }
                
            return instance;
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

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            SetFirstPage();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }
}
