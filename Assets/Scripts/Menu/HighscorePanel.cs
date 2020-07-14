using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HighscorePanel : MonoBehaviour
{
    public TextMeshProUGUI nameTMP, scoreTMP, modeTMP;
    public void Set(string name, string score, string mode)
    {
        nameTMP.text = name;
        scoreTMP.text = score;
        modeTMP.text = mode;
    }
}
