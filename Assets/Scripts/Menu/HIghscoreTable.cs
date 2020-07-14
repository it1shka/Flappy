using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HIghscoreTable : MonoBehaviour
{
    private string url = "http://localhost:5000";

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private IEnumerator SendHighscore()
    {
        var r = UnityWebRequest.Post()
    }
}
