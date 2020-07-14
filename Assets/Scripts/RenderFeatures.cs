using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static Controller.MODE;
public class RenderFeatures : MonoBehaviour
{
    public Material 
        reverseMat,
        fullReverseMat,
        fullreverse2Mat;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        switch (Controller.CurrentMode) {
            case NORMAL:
                Graphics.Blit(source, destination);
                break;
            case REVERSE:
                Graphics.Blit(source, destination, reverseMat);
                break;
            case FULLREVERSE:
                Graphics.Blit(source, destination, fullReverseMat);
                break;
            case FULLREVERSE2:
                Graphics.Blit(source, destination, fullreverse2Mat);
                break;

        }

    }

}

