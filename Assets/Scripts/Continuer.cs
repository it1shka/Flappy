using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Continuer : MonoBehaviour
{
    public GameObject tmpObject, defaultUI;
    public TextMeshProUGUI tmp;
    public PostProcessorSettings pps;
    public int count = 3;

    public AudioManager audioManager;

    public void Continue(bool disable)
    {
        StartCoroutine(CountDown(disable));
    }

    private IEnumerator CountDown(bool disable)
    {
        pps.BlurEnabled = false;
        pps.BlurRadius = 0f;
        tmpObject.SetActive(true);
        for(var i=0; i < count; i++)
        {
            tmp.text = $"{count - i}";
            audioManager.Play("pip");
            yield return new WaitForSecondsRealtime(1f);
        }
        Time.timeScale = 1f;
        Controller.isPaused = false;
        tmpObject.SetActive(false);
        defaultUI.SetActive(true);
        if(disable)
            FindObjectOfType<Controller>().DisableCollider();
        audioManager.Play("blip");
    }

}
