using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {


    private int maxForce = 100;
    private int force = 50;

    private Image healthBar;
    public int Force 
    { 
        get {return force;} 
        set {force = value;} 
    }

    public int MaxForce 
    { 
        get {return maxForce;} 
        set {maxForce = value;} 
    }

    private void Start() {
        healthBar = transform.Find("Health").GetComponent<Image>();
    }

    private void Update() {
        float healthPercentage = Mathf.Min(Mathf.Max(0, Force * 1f / MaxForce), 1);

        healthBar.fillAmount = healthPercentage;

    }
}
