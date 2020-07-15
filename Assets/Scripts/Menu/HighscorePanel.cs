using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HighscorePanel : MonoBehaviour
{
    public TextMeshProUGUI nameTMP, scoreTMP, modeTMP;
    private string[] modes = new string[] { 
        "<color=#008000>E</color>", 
        "<color=#FFFF00>H</color>",
        "<color=#FF0000>U</color>",
        "<color=#8B00FF>I</color>"
    };
    public void Set(HighscoreInfo info)
    {
        nameTMP.text = info.name;
        scoreTMP.text = $"{info.score}";
        modeTMP.text = modes[info.playmodeIndex];
    }
}
