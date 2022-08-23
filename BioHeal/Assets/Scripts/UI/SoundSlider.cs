using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{    
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private VolumeType volumeType;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(delegate { SoundManager.Instance.ChangeVolume(volumeType, volumeSlider.value); });
        switch (volumeType)
        {
            case VolumeType.Effects:
                volumeSlider.value = SoundManager.Instance.effectsVolume;
                break;
            case VolumeType.Music:
                volumeSlider.value = SoundManager.Instance.musicVolume;
                break;
            default:
                break;
        }
    }

    public enum VolumeType
    {
        Effects,
        Music
    }
}
