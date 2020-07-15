using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;
using System.Web;

public class HighscoreTable : MonoBehaviour
{
    private string url = "http://localhost:5000";
    public TextMeshProUGUI highscoreTMP;
    public TMP_InputField nameINP;
    public GameObject error;
    private HighscoreInfo highscoreInfo;

    public GameObject highscorePanelPrefab;
    public RectTransform canvas;
    void Awake()
    {
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

            StartCoroutine(SendHighscore());
            PlayerPrefs.SetInt("pushedrecord", highscore);
            PlayerPrefs.Save();
        }
        StartCoroutine(getTable());
    }
    
    private IEnumerator getTable()
    {
        var r = UnityWebRequest.Get(url);
        yield return r.SendWebRequest();
        if (r.isNetworkError)
            error.SetActive(true);
        var result = r.downloadHandler.text;
        print($"Got a json pack: {result}");
        var output = JsonConvert.DeserializeObject<HighscoreInfoOutput[]>(result);
        foreach(var elem in output)
        {
            var curInfo = JsonConvert.DeserializeObject<HighscoreInfo>(elem.info);
            Instantiate(highscorePanelPrefab, canvas)
                .GetComponent<HighscorePanel>()
                .Set(curInfo);
        }

    }

    private IEnumerator SendHighscore()
    {
        var serializedObj = JsonConvert.SerializeObject(highscoreInfo);
        var r = UnityWebRequest.Post(url, serializedObj);

        yield return r.SendWebRequest();
        if (r.isNetworkError)
            error.SetActive(true);
    }

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
    public HighscoreInfo(string playerName, int playerScore, int playmodeIndex)
    {
        this.name = playerName;
        this.score = playerScore;
        this.playmodeIndex = playmodeIndex;
    }
}

[System.Serializable]
public class HighscoreInfoOutput
{
    public string info;
    public string _id;
}