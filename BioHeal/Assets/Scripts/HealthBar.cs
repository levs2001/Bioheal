using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {


    private int maxForce = 100;
    private int force = 50;

    private Image healthBar;
    // private Image dropEffect;

    public int MaxForce 
    { 
        get {return maxForce;} 
        set {maxForce = value;} 
    }

    public int Force 
    { 
        get {return force;} 
        set {force = value;} 
    }

    private void Start() {
        healthBar = transform.Find("Health").GetComponent<Image>();
        healthBar.fillAmount = Mathf.Min(Mathf.Max(0, force / maxForce), 1);
    }

    // private void Update() {
    //     float healthPercentage = Mathf.Min(Mathf.Max(0, currentHealth / maxHealth), 1);

    //     healthBar.fillAmount = healthPercentage;

    // }
}
