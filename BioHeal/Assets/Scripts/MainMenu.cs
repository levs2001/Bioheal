using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void LoadGameScene()
    {
        // get loader to load json in advance    
        Loader loader = Loader.LoaderInstance;
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
