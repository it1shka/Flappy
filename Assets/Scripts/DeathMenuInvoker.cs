using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenuInvoker : MonoBehaviour
{
    public GameObject onDeathMenu;
    public float time = 3f;
    public void InvokeDeathMenu()
    {
        Invoke("SetActiveMenu", time);
    }
    public void SetActiveMenu()
    {
        onDeathMenu.SetActive(true);
    }
}
