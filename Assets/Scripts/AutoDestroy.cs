using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float dtime = 1f;
    void Start()
    {
        Destroy(gameObject, dtime);
    }

}
