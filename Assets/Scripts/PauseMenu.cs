using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public PostProcessorSettings pps;

    public void Pause()
    {
        Controller.isPaused = true;
        pps.BlurEnabled = true;
        pps.BlurRadius = .006f;
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        FindObjectOfType<Continuer>().Continue(false);
    }

    public void ToMenu()
    {
        Application.LoadLevel("Menu");
    }

}
