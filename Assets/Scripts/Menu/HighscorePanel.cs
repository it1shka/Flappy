using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighscorePanel : MonoBehaviour
{
    public TextMeshProUGUI nameTMP, scoreTMP, modeTMP;
    public RawImage myPanelImg;
    public Color myColor, notMyColor;
    private string[] modes = new string[] { 
        "<color=#008000>E</color>", 
        "<color=#FFFF00>H</color>",
        "<color=#FF0000>U</color>",
        "<color=#8B00FF>I</color>"
    };
    public void Set(HighscoreInfo info, string id)
    {
        nameTMP.text = info.name;
        scoreTMP.text = $"{info.score}";
        modeTMP.text = modes[info.playmodeIndex];
        myPanelImg.color = info.id == id ? myColor : notMyColor;
    }
}
