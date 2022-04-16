using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayBar : HealthDisplay
{
    private static Vector2 SIZE_DELTA = new Vector2(0.8f, 0.25f);
    private RectTransform rectTransform;
    private Image healthBar;

    protected override void Start()
    {
        base.Start();
        rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = SIZE_DELTA; // set healthbar size
        healthBar = transform.Find("Health").GetComponent<Image>();
        GetComponent<Canvas>().enabled = true;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        rectTransform.anchoredPosition = new Vector3(owner.transform.position.x, owner.transform.position.y + 0.5f, 0); // mb change to coordinate wise assignation
        healthBar.fillAmount = healthPercentage;
    }
}
