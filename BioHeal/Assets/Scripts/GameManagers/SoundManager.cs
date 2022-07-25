using System.Collections.Generic;
using System;
using UnityEngine;
using static Logger;

public class SoundManager : Logger
{
    private const string PathSounds = "Sounds/";

    private Dictionary<SoundType, AudioClip> sounds;

    [SerializeField] private AudioSource effectsSource;
    [SerializeField] private AudioSource musicSource;

    private static SoundManager instance = null;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
                throw new System.Exception("SoundManager not exist");
            else
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
            source = musicSource;
        else
            source = effectsSource;

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
