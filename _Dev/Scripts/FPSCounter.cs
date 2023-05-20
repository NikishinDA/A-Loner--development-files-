using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public static float fps;
    private GUIStyle style;

    private void Start()
    {
        style = new GUIStyle { fontSize = 50, fontStyle = FontStyle.Bold };
    }

    private void OnGUI()
    {
        fps = 1.0f / Time.deltaTime;
        int refreshRate = Screen.currentResolution.refreshRate;
        GUILayout.Label("FPS: " + (int)fps + " RR: " + refreshRate, style);
    }
}

