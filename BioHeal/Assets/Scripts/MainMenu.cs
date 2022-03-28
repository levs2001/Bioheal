using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void LoadGameScene()
    {
        var loader = Loader.LoaderInstance;
        // print(Loader.GetConfig().levels[0].mineral.initialC);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
