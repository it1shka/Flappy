using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathMenu : MonoBehaviour
{
    public GameObject defaultUI;
    public PostProcessorSettings pps;
    void Start()
    {
        defaultUI.SetActive(false);
        pps.BlurEnabled = true;
    }

    void Update()
    {
        pps.BlurRadius = Mathf.Lerp(pps.BlurRadius, .006f, Time.unscaledDeltaTime);
    }

    public void Replay()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void ToMenu()
    {
        Application.LoadLevel("Menu");
    }
}
