using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Awake()
    {
        PlayerPrefs.SetInt("record", 0);
        PlayerPrefs.Save();
    }

}
