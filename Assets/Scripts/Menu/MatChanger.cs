using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatChanger : MonoBehaviour
{
    public Material[] mats;
    private Material currentMat;
    private int i;
    public float changeTime;
    private float curTimeBtwChange;
    void Start()
    {
        i = -1;
        curTimeBtwChange = changeTime;
        Pick();
    }

    private void Update()
    {
        curTimeBtwChange -= Time.deltaTime;
        if(curTimeBtwChange <= 0f)
        {
            curTimeBtwChange = changeTime;
            Pick();
        }
    }

    void Pick()
    {
        i++;
        if (i >= mats.Length) i = 0;
        currentMat = mats[i];
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, currentMat);
    }
}
