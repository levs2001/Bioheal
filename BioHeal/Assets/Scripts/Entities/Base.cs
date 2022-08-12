using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SceneManager;
using static MetaInfo;
using static LogFactory;

public class Base : Alive
{
    //SerializeField, because I am initializing these fields from Unity API from inspector 
    [SerializeField] private Text textForceBase;
    private static readonly Log log = LogFactory.GetLog(typeof(Base));

    private static Base instance = null;
    public static Base Instance
    {
        get
        {
            if (instance == null)
                throw new System.Exception("Base does not exist");
            else
                return instance;
        }
    }

    public void ShowBaseButton()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);
        //Today we do not have job for this button
        Camera.main.transform.position = new Vector3(0, 0, Camera.main.transform.position.z);
    }
    private void ChangeForceText()
    {
        textForceBase.text = force < 0 ? $"{0}" : $"{force}";
    }

    private void Die()
    {
        //this action moved to EndLevel to remember timeScale to return it
        //after closing EndLevelMenu
        //Time.timeScale = 0;

        EndLevel.Instance.OpenLoseLevelMenu();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        //Init();
        instance = this;

        base.Start();
        textForceBase.text = $"{force}";

        entityTakeDamageEvent += ChangeForceText;
        entityTakeDamageEvent += (() => SoundManager.Instance.PlaySound(SoundManager.SoundType.HeartDamage));
        entityTakeDamageEvent += (() =>
        {
            if (force <= 0)
            {
                SoundManager.Instance.PlaySound(SoundManager.SoundType.HeartDead);
                Die();
            }
        });
    }
}