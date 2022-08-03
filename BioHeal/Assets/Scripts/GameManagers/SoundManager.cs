using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{
    private const string PathSounds = "Sounds/";

    private Dictionary<SoundType, AudioClip> sounds;

    [SerializeField] private AudioSource effectsSource;
    [SerializeField] private AudioSource musicSource;
    public float volume = 1.0f; // on scale from 0 to 1

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
        source.volume = volume;
        source.Play();
    }

    public void StopSound(SoundType type)
    {
        if (type == SoundType.MainTheme)
            musicSource.Stop();
        else
            effectsSource.Stop();
    }

    public void ChangeVolume(float value) {
        volume = value;
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
