using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    private static SoundManager instance = null;

    public static SoundManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private AudioClip GetSound(SoundType type)
    {
        foreach (var sound in sounds)
        {
            if (sound.type == type)
            {
                return sound.audioClip;
            }
        }

        return null;
    }

    public void PlaySoundEffect(SoundType type)
    {
        AudioSource effectsSource = gameObject.AddComponent<AudioSource>();
        effectsSource.clip = GetSound(type);
        effectsSource.Play();
        Destroy(effectsSource, effectsSource.clip.length);
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

    [System.Serializable]
    private struct Sound
    {
        public SoundType type;
        public AudioClip audioClip;
    }
}
