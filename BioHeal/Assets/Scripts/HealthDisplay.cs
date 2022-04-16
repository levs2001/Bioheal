using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

    private static Vector2 SIZE_DELTA = new Vector2 (0.8f, 0.25f);
    private int maxForce = 100;
    private int force = 50;
    private Unit owner;
    private Image healthBar;
    private RectTransform rectTransform;
    private Vector2 maxScale = new Vector2(0f, 0f);

    public Unit Owner 
    { 
        set {owner = value;}
    }
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
        MaxForce = owner.Force;
        Force = owner.Force;
        rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = SIZE_DELTA; // set healthbar size

        if (Loader.LoaderInstance.healthDisplayType == HealthDisplayType.MODEL_SIZE)
        {
            maxScale = owner.transform.localScale;
        }

        healthBar = transform.Find("Health").GetComponent<Image>();
        if (Loader.LoaderInstance.healthDisplayType != HealthDisplayType.BAR)
        {
            // healthBar.enabled = false;
            GetComponent<Canvas>().enabled = false;
        }
        // healthBar.enabled = (Loader.LoaderInstance.healthDisplayType == HealthDisplayType.BAR);
    }

    private void Update() {
        Force = owner.Force;
        float healthPercentage = Mathf.Min(Mathf.Max(0, Force * 1f / MaxForce), 1);

        rectTransform.anchoredPosition = new Vector3(owner.transform.position.x, owner.transform.position.y + 0.5f, 0); // mb change to coordinate wise assignation
        healthBar.fillAmount = healthPercentage;

        if (Loader.LoaderInstance.healthDisplayType == HealthDisplayType.MODEL_SIZE)
        {
            float scaleFactor = force * 1.0f / maxForce;
            this.transform.localScale = new Vector2(maxScale.x * scaleFactor, maxScale.y * scaleFactor);    
        }
    }


}
