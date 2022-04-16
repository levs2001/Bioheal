using System;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Toggle[] toggles = new Toggle[3];
    private Toggle selectedToggle;

    public void ToggleSelected(Toggle toggle)
    {
        selectedToggle = toggle;
        Loader.LoaderInstance.healthDisplayType = (HealthDisplayType)Array.IndexOf(toggles, toggle);
    }

    private void Start()
    {
        selectedToggle = toggles[(int)Loader.LoaderInstance.healthDisplayType];
        //TODO: it isn't dysplayed in ui, need to subscribe on change
    }

}
