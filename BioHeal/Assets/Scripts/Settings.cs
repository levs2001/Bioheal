using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Toggle[] toggles = new Toggle[3];
    private Toggle selectedToggle;

    public void ToggleSelected(Toggle toggle)
    {
        //TO DO: write this to json
        selectedToggle = toggle;
    }

    private void Start()
    {
        //TO DO: read it from json
        selectedToggle = toggles[0];
    }

}
