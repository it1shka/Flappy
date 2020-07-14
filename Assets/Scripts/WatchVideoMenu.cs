using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
public class WatchVideoMenu : MonoBehaviour, IUnityAdsListener
{
    public GameObject defaultUI;
    public float invokeDeathTime = 3f;

    private const string myAppId = "3569993";
    public string adsType = "rewardedVideo";
    public Button button;

    void Start()
    {
        defaultUI.SetActive(false);
        Controller.isPaused = true;
        Time.timeScale = 0f;
    }

    private void Awake()
    {
        button.interactable = Advertisement.IsReady(adsType);
        button.onClick.AddListener(WatchAdd);
        Advertisement.AddListener(this);
        Advertisement.Initialize(myAppId, false);
    }

    public void InvokeDeath()
    {
        Controller.isPaused = false;
        Time.timeScale = 1f;
        FindObjectOfType<Controller>().Death();
    }

    public void WatchAdd()
    {
        Advertisement.Show(adsType);
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == adsType)
        {
            button.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            //FindObjectOfType<Controller>().DisableCollider(); //as later
            Controller.isWatched = true;
            FindObjectOfType<Continuer>().Continue(true);
        }
        else if (showResult == ShowResult.Skipped)
        {
            InvokeDeath();
        }
        else if (showResult == ShowResult.Failed)
        {
            InvokeDeath();
        }
    }


    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {

    }
}
