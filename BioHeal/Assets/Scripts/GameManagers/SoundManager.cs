using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{
    private const string PathSounds = "Sounds/";

    private static readonly Log log = LogFactory.GetLog(typeof(SoundManager));

    private Dictionary<SoundType, AudioClip> sounds;

    [SerializeField] private AudioSource effectsSource;
    [SerializeField] private AudioSource musicSource;
    public float musicVolume = 1f; // on scale from 0 to 1
    public float effectsVolume = 1f; // on scale from 0 to 1

    private static SoundManager instance = null;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                log.Error(new System.Exception("SoundManager not exist"));
            }
            
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            sounds = new Dictionary<SoundType, AudioClip>();
            foreach (SoundType type in Enum.GetValues(typeof(SoundType)))
            {
                sounds[type] = Resources.Load<AudioClip>(PathSounds + type.ToString());
            }

            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(SoundType type)
    {
        AudioSource source;

        if (type == SoundType.MainTheme)
        {
            source = musicSource;
            source.volume = musicVolume;
        }
        else
        {
            source = effectsSource;
            source.volume = effectsVolume;
        }

        source.clip = sounds[type];
        source.Play();
    }

    public void StopSound(SoundType type)
    {
        if (type == SoundType.MainTheme)
            musicSource.Stop();
        else
            effectsSource.Stop();
    }

    public void ChangeVolume(SoundSlider.VolumeType volumeType, float value) 
    {
        switch (volumeType)
        {
            case SoundSlider.VolumeType.Effects:
                effectsVolume = value;
                break;
            case SoundSlider.VolumeType.Music:
                musicVolume = value;
                break;
            default:
                break;
        }
    }

    public enum SoundType
    {
        HeartTap,
        AnyTap,
        UnitSpawn,
        HeartDamage,
        HeartDead,
        MainTheme,
        UnitDead,
        UnitFight,
        MineralTake
    }
}
