using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

//IL2CPP does not support Newtonsoft.Json... Blyat
using Newtonsoft.Json;
using System.Web;

public class HighscoreTable : MonoBehaviour
{
    private string url = "https://finalflappyserver.oa.r.appspot.com/";
    public TextMeshProUGUI highscoreTMP;
    public TMP_InputField nameINP;
    public TextMeshProUGUI error, additionalError;
    private HighscoreInfo highscoreInfo;

    public GameObject highscorePanelPrefab;
    public RectTransform canvas;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        print("connecting...");
        var highscore = PlayerPrefs.GetInt("record");
        var pushedhighscore = PlayerPrefs.GetInt("pushedrecord");
        var name = PlayerPrefs.GetString("nickname");
        var mode = PlayerPrefs.GetInt("modeindex");
        highscoreInfo = new HighscoreInfo(name, highscore, mode);
        nameINP.onEndEdit.AddListener(delegate { INPOnEnd(); });
        nameINP.text = name;
        highscoreTMP.text = $"{highscore}";
        
        if (highscore > pushedhighscore)
        {
            StartCoroutine(getTable(true));
            PlayerPrefs.SetInt("pushedrecord", highscore);
            PlayerPrefs.Save();
        }
        else StartCoroutine(getTable(false));
    }
    
    private IEnumerator getTable(bool sendHighscore)
    {
        SetStatus("Connecting to my server...", "Please wait");
        var myId = PlayerPrefs.GetString("id");
        if (myId == "")
        {
            var rg = UnityWebRequest.Get($"{url}/id");
            yield return rg.SendWebRequest();
            if (rg.isNetworkError)
            {
                SetStatus("Unable to send GET_ID req", "Check your internet connection");
                yield break;
            }
            SetStatus("Got an id", "Done!");
            var myNewId = rg.downloadHandler.text;
            PlayerPrefs.SetString("id", myNewId);
            PlayerPrefs.Save();
            myId = myNewId;
        }
        highscoreInfo.id = myId;

        if (sendHighscore)
        {
            var serializedObj = JsonConvert.SerializeObject(highscoreInfo);
            var p = UnityWebRequest.Post(url, serializedObj);

            yield return p.SendWebRequest();
            if (p.isNetworkError) {
                SetStatus("Unable to send POST_SCORE req", "Check your internet connection");
                yield break;
            }
            SetStatus("Sent your highscore", "Done!");
            print("Inserting feedback:" + p.downloadHandler.text);
        }

        var r = UnityWebRequest.Get(url);
        yield return r.SendWebRequest();
        if (r.isNetworkError) {
            SetStatus("Unable to send GET_TABLE req", "Check your internet connection");
            yield break;
        }
        SetStatus("Got a responce", "Done!");

        try
        {
            var result = r.downloadHandler.text;
            print($"Got a json pack: {result}");

            var output = JsonConvert.DeserializeObject<HighscoreInfo[]>(result);
            foreach (var elem in output)
            {
                Instantiate(highscorePanelPrefab, canvas)
                    .GetComponent<HighscorePanel>()
                    .Set(elem, myId);
            }

            SetStatus("", "");
        }
        catch (System.Exception e)
        {
            SetStatus(e.Message, e.StackTrace);
        }
    }

    private void SetStatus(string main, string additional)
    {
        error.text = main;
        additionalError.text = additional;
    }
    /*
    private IEnumerator SendHighscore()
    {
        var serializedObj = JsonConvert.SerializeObject(highscoreInfo);
        var r = UnityWebRequest.Post(url, serializedObj);

        yield return r.SendWebRequest();
        if (r.isNetworkError)
            error.SetActive(true);

        print("Inserting feedback:" + r.downloadHandler.text);
    }
    */
    public void INPOnEnd()
    {
        if(nameINP.text.Length > 0)
        {
            PlayerPrefs.SetString("nickname", nameINP.text);
            PlayerPrefs.Save();
        }
        else
        {
            nameINP.text = PlayerPrefs.GetString("nickname");
        }
    }
}

[System.Serializable]
public class HighscoreInfo
{
    public string name;
    public int score;
    public int playmodeIndex;

    public string id;
    public HighscoreInfo(string playerName, int playerScore, int playmodeIndex)
    {
        this.name = playerName;
        this.score = playerScore;
        this.playmodeIndex = playmodeIndex;
        id = "";
    }
}
/*
[System.Serializable]
public class HighscoreInfoOutput
{
    public string info;
    public string _id;
}*/