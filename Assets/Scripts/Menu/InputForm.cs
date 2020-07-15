using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InputForm : MonoBehaviour
{
    public GameObject main;
    public TMP_InputField inputField;
    public Button confirm;
    void Start()
    {
        inputField.onValueChanged.AddListener(delegate { OnValueChanged(); });
        if (PlayerPrefs.GetString("nickname").Length > 0)
            GotoMain();
    }

    public void SetName()
    {
        PlayerPrefs.SetString("nickname", inputField.text);
        PlayerPrefs.Save();
        GotoMain();
    }

    

    private void GotoMain()
    {
        main.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnValueChanged()
    {
        confirm.interactable = inputField.text.Length > 0;
    }
}
