using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public string[] modes = new string[] {"EASY", "HARD", "ULTRA", "INSANE"};
    private int currentMode;
    public TextMeshProUGUI tmp;
    void Start()
    {
        //ашч игп bug fix
        Time.timeScale = 1f;

        currentMode = PlayerPrefs.GetInt("currentmode")-1;
        PickMode();
    }

    public void PickMode()
    {
        currentMode ++;
        if (currentMode >= modes.Length) currentMode = 0;
        tmp.text = modes[currentMode];
        PlayerPrefs.SetInt("currentmode", currentMode);
        PlayerPrefs.Save();
    }

    public void Play()
    {
        Application.LoadLevel("SampleScene");
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void OpenGitHub()
    {
        Application.OpenURL("https://github.com/it1shka/Flappy");
    }
}
