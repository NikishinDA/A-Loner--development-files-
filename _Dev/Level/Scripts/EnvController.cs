using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvController : MonoBehaviour
{
    [SerializeField] private GameObject[] envs;
    [SerializeField] private Material[] groundMats;
    [SerializeField] private Renderer groundRenderer;

    private void Awake()
    {
        int level = PlayerPrefs.GetInt(PlayerPrefsStrings.Level, 0) / 3 % 3;
        groundRenderer.material = groundMats[level];
        if (level!= 0)
            envs[level].SetActive(true);
        
    }
}
