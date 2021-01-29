using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    TrailRenderer tr;

    void Awake()
    {
        tr = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        ActivateRenderer();
    }

    public void ActivateRenderer(bool activate = true)
    {
        if (activate)
        {
            tr.enabled = true;
        }
        else
        {
            tr.enabled = false;
        }
    }
}
