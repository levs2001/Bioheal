using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    
    private static SoundSlider instance = null;
    [SerializeField] private Slider volumeSlider;

    public static SoundSlider Instance
    {
        get
        {
            if (instance == null)
                throw new System.Exception("SoundSlider not exist");
            else
                return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            volumeSlider.onValueChanged.AddListener(delegate { SoundManager.Instance.ChangeVolume(volumeSlider.value); });
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = SoundManager.Instance.volume;

    }
}
